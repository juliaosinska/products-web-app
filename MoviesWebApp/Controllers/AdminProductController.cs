using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using MoviesWebApp.Models;
using MoviesWebApp.Models.ViewModels;
using MoviesWebApp.Repositories;
using ProductsWebApp.Models.ViewModels;
using ProductsWebApp.Repositories;

namespace MoviesWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProductController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
		private readonly UserManager<IdentityUser> userManager;

		public AdminProductController(ICategoryRepository categoryRepository, IProductRepository productRepository, IMapper mapper,
			UserManager<IdentityUser> userManager)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
			this.userManager = userManager;
		}

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //pobranie kategorii z repository
            var category = await categoryRepository.GetAllAsync();

            var model = new AddProductRequest
            {
                Categories = category.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductRequest addProductRequest)
        {
            //mapowanie view model to domain model
            var product = mapper.Map<AddProductRequest, Product>(addProductRequest);

            product.CreationDate = DateTime.Now;
            product.CreatorUserId = Guid.Parse(userManager.GetUserId(User));

			//mapowanie wybranych categories (maja sie wyswitlac tylko te wybrane)
			var selectedCategories = new List<Category>();
            foreach(var selectedCategoryId in addProductRequest.SelectedCategories)
            {
                var selectedCategoryIdAsInt = int.Parse(selectedCategoryId);
                var existingCategory = await categoryRepository.GetAsync(selectedCategoryIdAsInt);

                if (existingCategory != null)
                {
                    selectedCategories.Add(existingCategory);
                }
            }

            //mapowanie z powrotem do domain model
            product.Categories = selectedCategories;

            await productRepository.AddAsync(product);

            return RedirectToAction("Add"); 
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var products = await productRepository.GetAllAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //pobranie informacji o produkcie, ktory admin chce zedytowac + informacji o dostepnych kategorii
            var product = await productRepository.GetAsync(id);
            var categoriesDomainModel = await categoryRepository.GetAllAsync();
            
            if (product != null)
            {
                //mapowanie domain model do view model
                var editProductRequest = mapper.Map<EditProductRequest>(product);
                
                //pobranie id przypisanych aktualnie kategorii
                var selectedCategoryIds = product.Categories.Select(c => c.Id).ToList();

                //wyswietlenie WSZYSTKICH kategorii dostepnych z zaznaczonymi tymi ktore sa przypisane
                editProductRequest.Categories = categoriesDomainModel
                    .Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString(),
                        Selected = selectedCategoryIds.Contains(c.Id)
                    })
                    .ToList();

                return View(editProductRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductRequest editProductRequest)
        {
            //mapowanie view model do domain model
            var productPostDomainModel = mapper.Map<Product>(editProductRequest);

            //mapowanie categoies do domain model
            var selectedCategories = new List<Category>();
            foreach (var selectedCategory in editProductRequest.SelectedCategories)
            {
                if (int.TryParse(selectedCategory, out var category))
                {
                    var foundCategory = await categoryRepository.GetAsync(category);

                    if (foundCategory != null)
                    {
                        selectedCategories.Add(foundCategory);
                    }
                }
            }

            productPostDomainModel.Categories = selectedCategories;

            var updatedProduct = await productRepository.UpdateAsync(productPostDomainModel);

            if (updatedProduct != null)
            {
                ViewBag.SuccessMessage = "Aktualizacja produktu zakonczona pomyslnie!";
                return RedirectToAction("Edit");
            }
            ViewBag.ErrorMessage = "Blad podczas edycji produktu. Sprobuj ponownie.";
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditProductRequest editProductRequest)
        {
            var deletedProduct = await productRepository.DeleteAsync(editProductRequest.Id);

            if (deletedProduct != null)
            {
                ViewBag.SuccessMessage = "Pomyslnie usunieto produkt!";
                return RedirectToAction("List");
            }

            ViewBag.ErrorMessage = "Blad podczas usuwania produktu. Sprobuj ponownie.";
            return RedirectToAction("Edit", new { id = editProductRequest.Id });
        }
    }
}
