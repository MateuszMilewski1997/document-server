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

namespace fakultet.Controllers
{
    [Route("api/[controller]")]
    //[Authorize (Policy = "urzednik")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CommentsController(DatabaseContext context)
        {
            _context = context;
        }

       

        // GET: api/Comments/email@email.com
        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<Documents>>> GetDocuments(string email)
        {
            var documents = await _context.Documents.Where(x => x.User_Mail == email).ToListAsync();

            

            return documents;
        }

        // PUT: api/Comments/wmail@email.com
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocuments(int? id, documentsComment documentsComment)
        {
            var documents = await _context.Documents.FindAsync(id);

            string newComment = documentsComment.Comment;

             //Documents dokument = new Documents();

            if (newComment != null)
            {
                documents.Comment = newComment;

                _context.Update(documents);
                _context.SaveChanges();
            }



            return NoContent();
        }

        

        

        private bool DocumentsExists(int? id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
    }
}
