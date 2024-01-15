namespace ProductsWebApp.Models.Domain
{
    public class ProductLike
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Guid UserId { get; set; }
    }
}
