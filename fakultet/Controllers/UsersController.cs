using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: api/Users - pobieranie całej lisy userów
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDTO>>> GetUsers()
        {
            List<UsersDTO> UserDetailsDTO = new List<UsersDTO>();
            var users = await _context.Users.ToListAsync();

            foreach (Users user in users)
                UserDetailsDTO.Add(new UsersDTO(user));

            return UserDetailsDTO;
        }

        // GET: api/Users/5 - pobieranie e-maila i loginu danego usera
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersDTO>> GetUsers(int id)
        {
            Users user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            UsersDTO usersDTO = new UsersDTO(user)
            {
                Login = user.Login,
                Email = user.Email
            };

            return usersDTO;
        }

        // POST: api/Users - Rejestracja usera
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(RegistrationCOM registrationCOM)
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

            Users user = new Users()
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
        public async Task<ActionResult<Users>> DeleteUsers(int id)
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
