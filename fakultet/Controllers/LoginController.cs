using System;
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
    public class LoginController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public LoginController(DatabaseContext context)
        {
            _context = context;
        }

        // POST: api/Users - Logowanie
        [HttpPost]
        public async Task<ActionResult> PostAccount(LoginCOM loginCOM)
        {
            Users user = await _context.Users.SingleOrDefaultAsync(x =>
               x.Login == loginCOM.Login && x.Password == loginCOM.Password
                );

            if (user == null)
                return BadRequest(new { message = "Invalid credentials." });

            return Ok();
        }
    }
}
