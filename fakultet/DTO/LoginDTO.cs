using fakultet.Models;
using System.IdentityModel.Tokens.Jwt;

namespace fakultet.DTO
{
    public class LoginDTO
    {
        public string Token { get; set; }
        public string Login { get; set; }

        public LoginDTO(Users User, JwtSecurityToken Token)
        {
            Login = User.Login;
            this.Token = new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
