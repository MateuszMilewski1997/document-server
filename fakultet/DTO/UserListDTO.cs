using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using fakultet.Models;

namespace fakultet.DTO
{
    public class UserListDTO
    {

        private Users user;

       
        public UserListDTO(Users user)
        {
            Id = user.Id;
            Login = user.Login;
            Email = user.Email;
           
            Role_Name = user.Role;
        }

        public int? Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
       // public JwtSecurityToken Token { get; internal set; }
        public int Role_Name { get; set; }



    }

}

