﻿using RentCaarsAPIs.Models;

namespace RentCaarsAPIs.Dtos.UserDtos
{
    public class UserRegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
      
    }
}
