using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Data;
using MoviesWebApp.Models;
using MoviesWebApp.Models.ViewModels;
using MoviesWebApp.Repositories;
using ProductsWebApp.Models.ViewModels;

namespace MoviesWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public AdminCategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddCategoryRequest addCategoryRequest)
        {
            //mapowanie AddCategoryRequest do Category Domain model
            var category = mapper.Map<AddCategoryRequest, Category>(addCategoryRequest);

            await categoryRepository.AddAsync(category);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //uzywamy dbcontext aby odczytac wszystkie kategorie
            var category = await categoryRepository.GetAllAsync();

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //pobranie informacji o kategorii, ktora admin chce zedytowac
            var category = await categoryRepository.GetAsync(id);

            if (category != null)
            {
                //mapowanie domain model na viewmodel
                var editCategoryRequest = mapper.Map<EditCategoryRequest>(category);

                return View(editCategoryRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryRequest editCategoryRequest)
        {
            var existingCategory = await categoryRepository.GetAsync(editCategoryRequest.Id);
            mapper.Map(editCategoryRequest, existingCategory);

            var updatedCategory = await categoryRepository.UpdateAsync(existingCategory);

            if (updatedCategory != null)
            {
				ViewBag.SuccessMessage = "Aktualizacja kategorii zakonczona pomyslnie!";
			}
            else
            {
				ViewBag.ErrorMessage = "Blad podczas edycji kategorii. Sprobuj ponownie.";
			}

            return RedirectToAction("Edit", new { id = editCategoryRequest.Id, name = editCategoryRequest.Name });
        }

        [HttpPost]
        public async Task <IActionResult> Delete(EditCategoryRequest editCategoryRequest)
        {
            var deletedCategory = await categoryRepository.DeleteAsync(editCategoryRequest.Id);

            if (deletedCategory != null)
            {
                ViewBag.SuccessMessage = "Pomyslnie usunieto kategorie!";
                return RedirectToAction("List");
            }

            ViewBag.ErrorMessage = "Błąd podczas usuwania kategorii. Sprobuj ponownie.";
            return RedirectToAction("Edit", new { id = editCategoryRequest.Id });
        }
    }
}
