﻿using fakultet.Models;

namespace fakultet.DTO
{
    public class UserListDTO
    {

        public int? Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public int Role_Name { get; set; }

        public UserListDTO(Users user)
        {
            Id = user.Id;
            Login = user.Login;
            Email = user.Email;
           
            Role_Name = user.Role;
        }
    }
}

