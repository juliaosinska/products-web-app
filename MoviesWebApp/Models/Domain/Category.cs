namespace MoviesWebApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int IsDeleted { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
