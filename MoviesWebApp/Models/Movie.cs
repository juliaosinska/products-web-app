namespace MoviesWebApp.Models
{
    public class Movie
    {
        public int id { get; set; }

        public string title { get; set; }
            
        public string description { get; set; }

        public int isDeleted { get; set; }

        public DateTime creationDate { get; set; }

        public int creatorUserId { get; set; }

        public string imageUrl { get; set; }

        public ICollection<Category> categories { get; set; }
    }
}
