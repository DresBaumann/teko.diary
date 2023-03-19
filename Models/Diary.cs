using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Teko.Diary.Models
{
	public class Diary
	{
		public int Id { get; set; }

		[Microsoft.Build.Framework.Required] public string Name { get; set; } = null!;

		public IdentityUser? User { get; set; } = null!;
		public virtual ICollection<Entry>? Entries { get; set; }
	}
}