using System;
using Domain.Interfaces;

namespace Domain.Enitities
{
    public class Admin : IUser
    {
        public bool CanViewAllNotes { get; set; }
        public bool CanManageUsers { get; set; }


        //Iuser
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

