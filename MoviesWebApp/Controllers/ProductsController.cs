using Microsoft.AspNetCore.Mvc;
using ProductsWebApp.Repositories;
using System.Runtime.CompilerServices;

namespace ProductsWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var product = await productRepository.GetAsync(id);

            return View(product);
        }
    }
}
