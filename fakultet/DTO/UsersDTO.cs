using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fakultet.Models;

namespace fakultet.DTO
{
    public class UsersDTO
    {
        public UsersDTO(Users user)
        {
            Id = user.Id;
            Login = user.Login;
            Email = user.Email;
        }

        public int? Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
