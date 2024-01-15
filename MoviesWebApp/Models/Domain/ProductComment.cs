namespace ProductsWebApp.Models.Domain
{
	public class ProductComment
	{
        public int Id { get; set; }

        public string Description { get; set; }

        public int ProductId { get; set; }

        public Guid UserId {  get; set; }

        public DateTime DateAdded { get; set; }
    }
}
