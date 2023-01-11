namespace Teko.Diary.Models;

public class Entry
{
	public int Id { get; set; }

	public DateTimeOffset Date { get; set; }

	public string Text { get; set; }
}