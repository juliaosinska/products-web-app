using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Models;
using MoviesWebApp.Repositories;
using ProductsWebApp.Models.ViewModels;
using ProductsWebApp.Repositories;
using System.Diagnostics;
using System.Linq;

namespace MoviesWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index(string searchString, int? categoryId, int? page)
        {
            var products = await productRepository.GetAllAsync();
            var categories = await categoryRepository.GetAllAsync();

            //domyslna wartosc comboboca
            string selectedCategoryName = "All Categories";

            if (categoryId.HasValue)
            {
                //filtrowanie po kategorii
                products = products.Where(p => p.Categories.Any(c => c.Id == categoryId.Value)).ToList();

                //pobranie nazwy wybranej kategorii
                var selectedCategory = categories.FirstOrDefault(c => c.Id == categoryId.Value);
                if (selectedCategory != null)
                {
                    selectedCategoryName = selectedCategory.Name;
                }
            }

            //wyszukiwanie po nazwie
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Title.Contains(searchString)).ToList();
            }

            //stronicowanie
            int pageSize = 5;
            //domyslna strona - 1
            int pageNumber = (page ?? 1);
            var paginatedProducts = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            //tworzymy nowy view model
            var model = new HomeViewModel
            {
                Products = paginatedProducts,
                Categories = categories,
                SearchString = searchString,
                SelectedCategoryName = selectedCategoryName,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalProducts = products.Count()
            };

            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
