namespace ProductsWebApp.Models.ViewModels
{
	public class ProductCommentViewModel
	{
		public int Id { get; set; }

        public string Description { get; set; }

		public DateTime DateAdded { get; set; }

        public string Username { get; set; }

		public Guid UserId { get; set; }
    }
}
