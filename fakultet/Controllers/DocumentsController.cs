using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fakultet.Models;
using fakultet.Comends;
using Microsoft.AspNetCore.Cors;
using AutoMapper;

namespace fakultet.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public DocumentsController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Documents  ++
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documents>>> GetDocuments()
        {
            return await _context.Documents.ToListAsync();
        }

        // GET: api/Documents/5 ++
        [HttpGet("{id}")]
        public async Task<ActionResult<Documents>> GetDocuments(int? id)
        {
            var documents = await _context.Documents.FindAsync(id);

            if (documents == null)
            {
                return NotFound();
            }

            return documents;
        }

        [HttpGet("user/{email}")]
        public async Task<ActionResult<IEnumerable<Documents>>> GetDocumentsByEmail(string email)
        {

            var documents = await _context.Documents.Where(x => x.User_Mail == email).ToListAsync();

            if (documents == null)
                return NotFound();

            return documents;
        }

        // PUT: api/Documents/5     ----
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocuments(int? id, Documents documentStatus)
        {

            var document = await _context.Documents.FindAsync(id);

            if (document != null)
            {
                document.Status = documentStatus.Status;
               
                _context.SaveChanges();
            }
            else
            {
                return BadRequest("Row not found");
            }

            return Ok();

        }

        // POST: api/Documents  ==dorobic
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Documents>>> PostDocuments([FromBody] IEnumerable<DocumentsCOM> documentsCOM)
        {
            DateTime now1 = DateTime.Now;
            string strDate = now1.ToString();

            var documents = _mapper.Map<IEnumerable<Documents>>(documentsCOM);

            foreach(var d in documents)
                _context.Documents.Add(d);

            await _context.SaveChangesAsync();

            return Created("/documents", null);
        }

        // DELETE: api/Documents/5  ++
        [HttpDelete("{id}")]
        public async Task<ActionResult<Documents>> DeleteDocuments(int? id)
        {
            var documents = await _context.Documents.FindAsync(id);
            if (documents == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(documents);
            await _context.SaveChangesAsync();

            return documents;
        }

        private bool DocumentsExists(int? id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
    }
}
