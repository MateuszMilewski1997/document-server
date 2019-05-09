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
using Microsoft.Extensions.Caching.Memory;
using System.Net.Mail;

namespace fakultet.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    public class EmailValidationController : ControllerBase
    {

        private readonly DatabaseContext _context;
        private readonly IMemoryCache _cache;


        public EmailValidationController(DatabaseContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;

        }

        [HttpPost("{email}")]
        public async Task<ActionResult> CreateValidation(string email)
        {
            if (_context.Documents.Any(e => e.User_Mail == email))
            {
                
                var random = new Random();

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var stringChars = new char[8];


                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                string key = new String(stringChars);

                _cache.Set("key", key, TimeSpan.FromMinutes(180));
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";
                client.Port = 587;

                // setup Smtp authentication
                System.Net.NetworkCredential credentials =
                    new System.Net.NetworkCredential("FakultetBillenium@gmail.com", "haslo4321");
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("FakultetBillenium@gmail.com");

                //msg.To.Add(new MailAddress("milewskimateusz28@gmail.com"));
                msg.To.Add(new MailAddress(email));


                msg.Subject = "System zarz¹dzania obiegiem dokumentów";
                msg.IsBodyHtml = true;
                msg.Body = string.Format($"<html><head></head><body><b>Twój klucz: {key} </b>   </body>");

                client.Send(msg);
            }

            return Ok();
        }
    }

}