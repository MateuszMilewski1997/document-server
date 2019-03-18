using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fakultet.Models;
using fakultet.Comends;
using fakultet.DTO;

namespace fakultet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDTO>>> GetUsers()
        {

            List<UsersDTO> UserDetailsDTO = new List<UsersDTO>();
            var users = await _context.Users.ToListAsync();

            foreach (User adv in users)
                UserDetailsDTO.Add(new UsersDTO(adv));


            return UserDetailsDTO;
            //return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersDTO>> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
                return NotFound();

            UsersDTO usersDTO = new UsersDTO()
            {
                Id = users.Id,
                Login = users.Login,
                Email = users.Email
            };

            return usersDTO;
        }
        /*
        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/


        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUsers(RegistrationCOM registrationCOM)
        {
            int roleNumber = 0;

            if (registrationCOM.Role == "admin")
            {
                roleNumber = 1;
            }
            else if(registrationCOM.Role == "urzednik")
            {
                roleNumber = 2;
            }
            else if (registrationCOM.Role == "petent")
            {
                roleNumber = 3;
            }
            else if (registrationCOM.Role == "skargi")
            {
                roleNumber = 4;
            }
            else if (registrationCOM.Role == "podania")
            {
                roleNumber = 5;
            }



            User user = new User()
            {
                Id = null,
                Login = registrationCOM.Login,
                Password = registrationCOM.Password,
                Email = registrationCOM.Email,
                Role = roleNumber
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Created("User", user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }


    


}
