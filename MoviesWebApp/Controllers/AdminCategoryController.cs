using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Data;
using MoviesWebApp.Models;
using MoviesWebApp.Models.ViewModels;
using MoviesWebApp.Repositories;

namespace MoviesWebApp.Controllers
{
    public class AdminCategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public AdminCategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
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
            var category = new Category
            {
                name = addCategoryRequest.Name
            };

            await categoryRepository.AddAsync(category);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //używamy dbcontext aby odczytac kategorie
            var category = await categoryRepository.GetAllAsync();

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoryRepository.GetAsync(id);

            if (category != null)
            {
                var editCategoryRequest = new EditCategoryRequest
                {
                    id = category.id,
                    name = category.name
                };
                return View(editCategoryRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryRequest editCategoryRequest)
        {
            var category = new Category
            {
                id = editCategoryRequest.id,
                name = editCategoryRequest.name
            };

            var updatedCategory = await categoryRepository.UpdateAsync(category);

            if (updatedCategory != null)
            {
                //sukces
            }
            else
            {
                //error
            }

            return RedirectToAction("Edit", new { id = editCategoryRequest.id });
        }

        [HttpPost]
        public async Task <IActionResult> Delete(EditCategoryRequest editCategoryRequest)
        {
            var deletedCategory = await categoryRepository.DeleteAsync(editCategoryRequest.id);

            if (deletedCategory != null)
            {
                //sukces
                return RedirectToAction("List");
            }

            //powiadomienie o niepowodzeniu
            return RedirectToAction("Edit", new { id = editCategoryRequest.id });
        }
    }
}
