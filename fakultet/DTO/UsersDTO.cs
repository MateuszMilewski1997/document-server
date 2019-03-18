using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fakultet.Models;

namespace fakultet.DTO
{
    public class UsersDTO
    {
       // private User adv;

        public UsersDTO()
        {
            
        }

        public UsersDTO(User adv)
       {
            Id = adv.Id;
            Login = adv.Login;
            Email = adv.Email;
            //this.adv = adv;
        }

        public int? Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }

        
    }
}
