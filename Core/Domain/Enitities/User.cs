using System;
using Application.Interfaces;

namespace Domain.Enitities
{
	public class User :IUser
	{
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

