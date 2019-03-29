using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using fakultet.Models;

namespace fakultet.DTO
{
    public class UsersDTO
    {
        private Users user;

        public UsersDTO(Users user)
        {
            this.user = user;
        }

        public UsersDTO(Users user, System.IdentityModel.Tokens.Jwt.JwtSecurityToken token)
        {
            Id = user.Id;
            Login = user.Login;
            Email = user.Email;
            Token =  token;
            Role_Name = user.Role;
        }

        public int? Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public JwtSecurityToken Token { get; internal set; }
        public int Role_Name { get; set; }
    }
}
