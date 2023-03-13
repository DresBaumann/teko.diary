using System.ComponentModel.DataAnnotations;

namespace Teko.Diary.Models;

public class Entry
{
	public int Id { get; set; }

	[Required] public DateTimeOffset Date { get; set; }

	[Required] [StringLength(1000)] public string Text { get; set; } = null!;

	public List<Tag> Tags { get; set; } = null!;

	public Diary Diary { get; set; } = null!;

	public string? ImagePath { get; set; }
}