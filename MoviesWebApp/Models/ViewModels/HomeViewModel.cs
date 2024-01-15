using MoviesWebApp.Models;

namespace ProductsWebApp.Models.ViewModels
{
	public class HomeViewModel
	{
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public string SearchString { get; set; }

        public string SelectedCategoryName { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalProducts { get; set; }
    }
}
