using Microsoft.AspNetCore.Mvc;
using MoviesWebApp.Models;
using MoviesWebApp.Repositories;
using ProductsWebApp.Models.ViewModels;
using ProductsWebApp.Repositories;
using System.Diagnostics;

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

        public async Task<IActionResult> Index()
        {
            var products = await productRepository.GetAllAsync();

            var categories = await categoryRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                Products = products,
                Categories = categories
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
