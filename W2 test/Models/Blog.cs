namespace W2_test.Models
{
	public class Blog
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }
	}

}
