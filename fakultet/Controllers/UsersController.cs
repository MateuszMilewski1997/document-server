using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fakultet.Models;
using fakultet.Comends;
using fakultet.DTO;
using Microsoft.AspNetCore.Cors;

namespace fakultet.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
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
        public async Task<ActionResult<IEnumerable<UserListDTO>>> GetUsers()
        {
            List<UserListDTO> UserDetailsDTO = new List<UserListDTO>();
            var users = await _context.Users.ToListAsync();

            foreach (Users user in users)
                UserDetailsDTO.Add(new UserListDTO(user));

            return UserDetailsDTO;
        }

        // GET: api/Users/5 - pobieranie e-maila i loginu danego usera
        [HttpGet("{id}")]
        public async Task<ActionResult<UserListDTO>> GetUsers(int id)
        {
            Users user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            UserListDTO usersDTO = new UserListDTO(user)
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
            Roles Role = await _context.Roles.SingleOrDefaultAsync(x => x.Role_Name == registrationCOM.Role);
            if (Role == null)
                return BadRequest(new { message = "Role name doesn't exist" });

            Users UserLogin = await _context.Users.SingleOrDefaultAsync(x => x.Login == registrationCOM.Login);
            if (UserLogin != null)
                return BadRequest(new { message = "Login or e-mail exits in databse" });

            Users UserEmail = await _context.Users.SingleOrDefaultAsync(x => x.Email == registrationCOM.Email);
            if (UserEmail != null)
                return BadRequest(new { message = "Login or e-mail exits in databse" });

            if (registrationCOM.Password.Length < 8)
                return BadRequest(new { message = "Password must have minimum 8 characters" });

            Users user = new Users()
            {
                Id = null,
                Login = registrationCOM.Login,
                Password = registrationCOM.Password,
                Email = registrationCOM.Email,
                Role = Role.Id
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Created("/account", null);
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
