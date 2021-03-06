﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fakultet.Models;
using fakultet.Comends;

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
        public async Task<ActionResult<DocumentStatusCOM>> PostDocuments(DocumentsCOM documentsCOM)
        {

            //var date = DateTime.UtcNow;
            //string gDate ToString(date);
            DateTime now1 = DateTime.Now;
            string strDate = now1.ToString();

            DocumentStatusCOM document = new DocumentStatusCOM()
            {
                Id = null,
                Name_Doc = documentsCOM.Name_Doc,
                User_Mail = documentsCOM.User_Mail,
                Type_document = documentsCOM.Type_document,
                Function_author = documentsCOM.Function_author,
                
                Send_Date = strDate,
                Document_Description = documentsCOM.Document_Description,
                Status = documentsCOM.Status
            };



           
           

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();


            return Created("Doc", document);
            //return CreatedAtAction("GetDocuments", new { id = documentsCOM.Name_Doc }, documentsCOM);
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
