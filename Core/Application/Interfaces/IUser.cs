using System;
using Application.DTOs;

namespace Application.Interfaces
{
	public interface IUser
	{

		int Id { get; set; }

		string Username { get; set; }

        string Email{ get; set; }

        string Password { get; set; }

      
    }
   

}

