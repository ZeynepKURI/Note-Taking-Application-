﻿using System;
namespace Domain.Enitities
{
	public class Note
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string  Content { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}

