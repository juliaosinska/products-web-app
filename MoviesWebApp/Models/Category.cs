namespace MoviesWebApp.Models
{
    public class Category
    {
        public int id { get; set; }
        
        public string name { get; set; }

        public ICollection<Movie> movies { get; set; }

    }
}
