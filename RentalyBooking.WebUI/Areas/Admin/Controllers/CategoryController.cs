using Microsoft.AspNetCore.Mvc;
using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.EntityLayer.Entities;
using System.Threading.Tasks;

namespace RentalyBooking.WebUI.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> CategoryList()
        {
            var values = await _categoryService.TGetCategoriesWithCars();
            return View(values);
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.TDeleteAsync(id);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(Category entity)
        {
            await _categoryService.TInsertAsync(entity);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var value = await _categoryService.TGetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(Category entity)
        {
            await _categoryService.TUpdateAsync(entity);
            return RedirectToAction("CategoryList");
        }

        public async Task<IActionResult> GetCategoryById(int id)
        {
            var value = await _categoryService.TGetByIdAsync(id);
            return View(value);
        }

    }
}
