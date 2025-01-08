using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
	public class RegisterDTO
	{
		public string Username { get; set; }

		public string Email { get; set; }

     	public string Password { get; set; }

		[Required, Compare(nameof(Password))]  //Compare atributu, iki farklı özelliğin (property) eşit olup olmadığını doğrular.
        public string ConfirmPassword { get; set; } = string.Empty;
	}
}

