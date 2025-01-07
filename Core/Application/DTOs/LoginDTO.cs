using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
	public class LoginDTO
	{
		public int Id { get; set; }
		[Required, EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}

