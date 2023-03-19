using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Teko.Diary.Models;

namespace Teko.Diary.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Teko.Diary.Models.Tag> Tag { get; set; }
		public DbSet<Teko.Diary.Models.Diary> Diary { get; set; }
		public DbSet<Teko.Diary.Models.Entry> Entry { get; set; }
		public DbSet<Teko.Diary.Models.TagAssignment> TagAssignment { get; set; }
	}
}