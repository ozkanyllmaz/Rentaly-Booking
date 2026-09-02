using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.EntityLayer.Entities;

namespace RentalyBooking.WebUI.Areas.Admin.Controllers
{
    public class CarController : BaseController
    {
        private readonly ICarService _carService;
        private readonly IBrandService _brandService;
        private readonly ICarModelService _carModelService;
        private readonly ICategoryService _categoryService;
        private readonly IBranchService _branchService;
        private readonly IRentalyService _rentalService;

        public CarController(ICarService carService, IBrandService brandService, ICarModelService carModelService, ICategoryService categoryService, IBranchService branchService, IRentalyService rentalService)
        {
            _carService = carService;
            _brandService = brandService;
            _carModelService = carModelService;
            _categoryService = categoryService;
            _branchService = branchService;
            _rentalService = rentalService;
        }

        public async Task<IActionResult> CarList()
        {
            var values = await _carService.TGetCarsWithBrands();
            return View(values);
        }

        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carService.TDeleteAsync(id);
            return RedirectToAction("CarList");
        }

        [HttpGet]
        public async Task<IActionResult> CreateCar()
        {
            ViewBag.Brands = new SelectList(await _brandService.TGetListAsync(), "BrandId", "BrandName");
            ViewBag.Categories = new SelectList(await _categoryService.TGetListAsync(), "CategoryId", "CategoryName");
            ViewBag.Branches = new SelectList(await _branchService.TGetListAsync(), "BranchId", "BranchName");

            ViewBag.Models = new SelectList(new List<CarModel>(), "CarModelId", "ModelName");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetModelsByBrand(int brandId)
        {
            var models = await _carModelService.TGetCarModelByBrand(brandId);
            return Json(models);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCar(Car entity)
        {
            await _carService.TInsertAsync(entity);
            return RedirectToAction("CarList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCar(int id)
        {
            ViewBag.Brands = new SelectList(await _brandService.TGetListAsync(), "BrandId", "BrandName");
            ViewBag.Categories = new SelectList(await _categoryService.TGetListAsync(), "CategoryId", "CategoryName");
            ViewBag.Branches = new SelectList(await _branchService.TGetListAsync(), "BranchId", "BranchName");

            ViewBag.Models = new SelectList(await _carModelService.TGetListAsync(), "CarModelId", "ModelName");
            var value = await _carService.TGetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCar(Car entity)
        {
            await _carService.TUpdateAsync(entity);
            return RedirectToAction("CarList");
        }

        public async Task<IActionResult> GetCarById(int id)
        {
            var value = await _carService.TGetByIdAsync(id);
            return View(value);
        }

        [HttpGet]
        public async Task<IActionResult> CarDetail(int id)
        {
            var value = await _carService.TGetCarWithBrand(id);
            return View(value);
        }

        public async Task<IActionResult> GetCarsByBranchId(int id)
        {
            var values = await _carService.TGetCarsByBranchId(id);
            return View("CarList", values);
        }

        public async Task<IActionResult> CarsByCategory(int id)
        {
            var values = await _carService.TGetCarsByCategory(id);
            return View("CarList", values);
        }

        // Tarihlere göre müsait olan araçları getiren metot
        public async Task<IActionResult> GetAvailableCarsByDates(DateTime pickup, DateTime returnDate)
        {
            var values = await _carService.TGetAvailableCarsByDates(pickup, returnDate);
            return View("CarList", values);
        }

        public async Task<JsonResult> GetDisabledDates(int carId)
        {
            try
            {
                var rentals = await _rentalService.TGetListAsync();

                if (rentals == null)
                    return Json(new List<object>());

                var disabledRanges = rentals
                    .Where(x => x.CarId == carId && x.Status != "İptal" && x.Status != "Tamamlandı")
                    .Select(x => new
                    {
                        from = x.PickupDate.ToString("yyyy-MM-dd"),
                        to = x.ReturnDate.ToString("yyyy-MM-dd")
                    })
                    .ToList();

                return Json(disabledRanges);
            }
            catch (Exception ex)
            {
                // Hatayı logla ve JSON olarak dön
                return Json(new { error = ex.Message, inner = ex.InnerException?.Message });
            }
        }
    }
}
