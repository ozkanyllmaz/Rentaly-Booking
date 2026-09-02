using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.EntityLayer.Entities;
using System.Threading.Tasks;

namespace RentalyBooking.WebUI.Areas.Admin.Controllers
{
    public class CarModelController : BaseController
    {
        private readonly ICarModelService _carModelService;
        private readonly IBrandService _brandService;

        public CarModelController(ICarModelService carModelService, IBrandService brandService)
        {
            _carModelService = carModelService;
            _brandService = brandService;
        }

        public async Task<IActionResult> CarModelList()
        {
            var values = await _carModelService.TGetCarModelsWithBrand();
            return View(values);
        }

        public async Task<IActionResult> DeleteCarModel(int id)
        {
            await _carModelService.TDeleteAsync(id);
            return RedirectToAction("CarModelList");
        }

        [HttpGet]
        public async Task<IActionResult> CreateCarModel()
        {
            ViewBag.Brands = new SelectList(await _brandService.TGetListAsync(), "BrandId", "BrandName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCarModel(CarModel entity)
        {
            await _carModelService.TInsertAsync(entity);
            return RedirectToAction("CarModelList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCarModel(int id)
        {
            ViewBag.Brands = new SelectList(await _brandService.TGetListAsync(), "BrandId", "BrandName");
            var value = await _carModelService.TGetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCarModel(CarModel entity)
        {
            await _carModelService.TUpdateAsync(entity);
            return RedirectToAction("CarModelList");
        }

        public async Task<IActionResult> GetCarModelById(int id)
        {
            var value = await _carModelService.TGetByIdAsync(id);
            return View(value);
        }
    }
}
