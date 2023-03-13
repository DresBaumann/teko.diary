using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace Teko.Diary.Models
{
	public class Diary
	{
		public int Id { get; set; }

		[Required] public string Name { get; set; } = null!;

		public IdentityUser User { get; set; } = null!;
		public List<Entry>? Entries { get; set; }
	}
}