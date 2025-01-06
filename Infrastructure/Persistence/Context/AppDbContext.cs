using System;
using Domain.Enitities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
	public class AppDbContext : DbContext
	{
	public AppDbContext(DbContextOptions<AppDbContext>options): base (options) { }
		public DbSet<Note> notes { get; set; }
		public DbSet<User> users { get; set; }
		public DbSet<Admin> admins { get; set; }

	}
}

