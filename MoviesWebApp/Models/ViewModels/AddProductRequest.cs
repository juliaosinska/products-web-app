using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace MoviesWebApp.Models.ViewModels
{
    public class AddProductRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        //wyswietlenie kategorii
        public IEnumerable<SelectListItem> Categories { get; set; }

        //pobranie kategorii
        public string[] SelectedCategories { get; set; } = Array.Empty<string>();



    }
}
