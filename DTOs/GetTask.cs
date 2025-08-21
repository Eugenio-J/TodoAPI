namespace TodoAPI.DTOs
{
	public class GetTask
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public bool isCompleted { get; set; }
	}
}
