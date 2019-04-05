using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fakultet.Models;
using fakultet.Comends;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace fakultet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public DocumentsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Documents  ++
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentStatusCOM>>> GetDocuments()
        {
            return await _context.Documents.ToListAsync();
        }

        // GET: api/Documents/5 ++
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentStatusCOM>> GetDocuments(int? id)
        {
            var documents = await _context.Documents.FindAsync(id);

            if (documents == null)
            {
                return NotFound();
            }

            return documents;
        }

        // PUT: api/Documents/5     ----
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocuments(int? id, DocumentStatusCOM documentStatus)
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<DocumentStatusCOM>>> PostDocuments([FromBody] IEnumerable<DocumentsCOM> documentsCOM)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var user = _context.Users.SingleOrDefaultAsync(x => x.Id == id);

            foreach (DocumentsCOM doc in documentsCOM)
            {
                DateTime now1 = DateTime.Now;
                string strDate = now1.ToString();

                DocumentStatusCOM document = new DocumentStatusCOM()
                {
                    Id = null,
                    Name_Doc = doc.Name_Doc,
                    User_Mail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    Type_document = doc.Type_document,
                    Function_author = doc.Function_author,

                    Send_Date = strDate,
                    Document_Description = doc.Document_Description,
                    Status = doc.Status
                };

                _context.Documents.Add(document);
            }

            await _context.SaveChangesAsync();


            return Created("/documents", null);
        }

        // DELETE: api/Documents/5  ++
        [HttpDelete("{id}")]
        public async Task<ActionResult<DocumentStatusCOM>> DeleteDocuments(int? id)
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
