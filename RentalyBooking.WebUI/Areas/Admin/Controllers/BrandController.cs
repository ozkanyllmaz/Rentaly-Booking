using Microsoft.AspNetCore.Mvc;
using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.DataAccessLayer.Abstract;
using RentalyBooking.EntityLayer.Entities;

namespace RentalyBooking.WebUI.Areas.Admin.Controllers
{
    public class BrandController : BaseController
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IActionResult> BrandList()
        {
            var values = await _brandService.TGetListAsync();
            return View(values);
        }

        public async Task<IActionResult> DeleteBrand(int id)
        {
            await _brandService.TDeleteAsync(id);
            return RedirectToAction("BrandList");
        }

        [HttpGet]
        public async Task<IActionResult> CreateBrand()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBrand(Brand entity)
        {
            await _brandService.TInsertAsync(entity);
            return RedirectToAction("BrandList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBrand(int id)
        {
            var value = await _brandService.TGetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBrand(Brand entity)
        {
            await _brandService.TUpdateAsync(entity);
            return RedirectToAction("BrandList");
        }

        public async Task<IActionResult> GetBrandById(int id)
        {
            var value = await _brandService.TGetByIdAsync(id);
            return View(value);
        }
    }
}
