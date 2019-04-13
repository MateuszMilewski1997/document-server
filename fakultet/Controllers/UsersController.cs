using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fakultet.Models;
using fakultet.Comends;
using fakultet.DTO;
using Microsoft.AspNetCore.Cors;
using System.Net.Mail;
using System;

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
        public async Task<ActionResult<Users>> PostUsers([FromBody] RegistrationCOM registrationCOM)
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

            //-----------------------------------------------------------------------------------


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
            msg.To.Add(new MailAddress("milewskimateusz28@gmail.com"));

            msg.Subject = "System zarządzania obiegiem dokumentów";
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body><b>Twoje konto zostało utworzone.</b>   <br/><br/><br/> Wiadomość została wygenerowana automatycznie. Proszę na nią nie odpowiadać.</body>");

                client.Send(msg);



            //-----------------------------------------------------------------------------------

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
