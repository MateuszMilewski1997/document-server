using fakultet.Models;
using System.IdentityModel.Tokens.Jwt;

namespace fakultet.DTO
{
    public class LoginDTO
    {
        public string Token { get; set; }
        public int? UserId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public LoginDTO(Users User, JwtSecurityToken Token, string Role)
        {
            UserId = User.Id;
            Email = User.Email;
            this.Role = Role;
            Login = User.Login;
            this.Token = new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
