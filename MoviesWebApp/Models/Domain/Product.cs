namespace MoviesWebApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; }
            
        public string Description { get; set; }

        public int IsDeleted { get; set; }

        public DateTime CreationDate { get; set; }

        public int CreatorUserId { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
