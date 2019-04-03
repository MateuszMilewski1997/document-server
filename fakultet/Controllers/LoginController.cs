using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fakultet.Models;
using fakultet.Comends;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using fakultet.DTO;

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
        [HttpGet]
        public async Task<ActionResult> GetAccount(LoginCOM loginCOM)
        {
            Users user = await _context.Users.SingleOrDefaultAsync(x =>
               x.Login == loginCOM.Login && x.Password == loginCOM.Password
                );

            if (user == null)
                return BadRequest(new { message = "Invalid credentials." });

            string securityKey = "super_top-Security^KEY-03*03*2019.smesk.io";
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            Roles Role = await _context.Roles.SingleOrDefaultAsync(x => x.Id == user.Role);

            var claim = new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, Role.Role_Name)
             };

            var token = new JwtSecurityToken(
                issuer: "smesk.in",
                audience: "readers",
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: signingCredentials,
                claims: claim
                );

            LoginDTO usersDTO = new LoginDTO(user, token);

            return Ok(usersDTO);
        }
    }
}
