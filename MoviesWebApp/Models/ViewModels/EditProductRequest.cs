using Microsoft.AspNetCore.Mvc.Rendering;
using MoviesWebApp.Models;

namespace ProductsWebApp.Models.ViewModels
{
    public class EditProductRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int IsDeleted { get; set; }

        public DateTime CreationDate { get; set; }

        public int CreatorUserId { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public string[] SelectedCategories { get; set; } = Array.Empty<string>();
    }
}
