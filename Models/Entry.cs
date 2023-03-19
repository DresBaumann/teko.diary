using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teko.Diary.Models;

public class Entry
{
	public int Id { get; set; }

	[Required] public DateTime Date { get; set; }
	[Required] [StringLength(20)] public string Title { get; set; } = null!;
	[Required] [StringLength(1000)] public string Text { get; set; } = null!;

	[MaxLength(3)] public virtual ICollection<Tag>? Tags { get; set; }


	public virtual int DiaryId { get; set; }
	public virtual Diary? Diary { get; set; }
	public string? ImagePath { get; set; }
}