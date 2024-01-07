namespace MoviesWebApp.Models.ViewModels
{
    public class EditCategoryRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int IsDeleted { get; set; }
    }
}
