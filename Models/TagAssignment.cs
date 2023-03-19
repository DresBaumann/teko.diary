namespace Teko.Diary.Models
{
	public class TagAssignment
	{
		public int Id { get; set; }
		public int TagId { get; set; }
		public Tag Tag { get; set; }
		public int EntryId { get; set; }
		public Entry Entry { get; set; }
	}
}