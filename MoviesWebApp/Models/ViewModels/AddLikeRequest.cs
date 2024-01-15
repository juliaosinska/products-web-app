namespace ProductsWebApp.Models.ViewModels
{
	public class AddLikeRequest
	{
        public int ProductId { get; set; }

		public Guid UserId { get; set; }
    }
}
