using System;
namespace Application.DTOs
{
	public class NoteDTO
	{
		public int Id{ get; set; }

        public string Title{ get; set; }

        public string Content { get; set; }

        public int INote { get; set; }

        public DateTime CreatedAt{ get; set; }
    }
}

