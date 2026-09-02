using Microsoft.AspNetCore.Mvc;
using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.BusinessLayer.Concrete;
using RentalyBooking.DataAccessLayer.Abstract;
using RentalyBooking.WebUI.Models;
using System.Threading.Tasks;

namespace RentalyBooking.WebUI.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IBranchService _branchService;
        private readonly ICategoryService _categoryService;
        private readonly IFuelTypeService _fuelTypeService;

        public CarController(ICarService carService, IBranchService branchService, ICategoryService categoryService, IFuelTypeService fuelTypeService)
        {
            _carService = carService;
            _branchService = branchService;
            _categoryService = categoryService;
            _fuelTypeService = fuelTypeService;
        }

        public async Task<IActionResult> List(int CategoryId, int PickupBranch, int ReturnBranch, string pickupDateStr, string pickupTimeStr, string returnDateStr, string returnTimeStr)
        {
            // String olarak gelen tarih ve saati DateTime formatına güvenli bir şekilde çeviriyoruz
            DateTime PickupDate = CombineDateAndTime(pickupDateStr, pickupTimeStr);
            DateTime ReturnDate = CombineDateAndTime(returnDateStr, returnTimeStr);

            var cars = await _carService.TGetAvailableCarsByFilters(CategoryId, PickupBranch, ReturnBranch, PickupDate, ReturnDate);

            var pickupBranchObj = await _branchService.TGetByIdAsync(PickupBranch);
            var returnBranchObj = await _branchService.TGetByIdAsync(ReturnBranch);
            var category = await _categoryService.TGetByIdAsync(CategoryId);

            CarSearchResultViewModel viewModel = new CarSearchResultViewModel
            {
                Cars = cars,
                CategoryName = category.CategoryName,
                PickupBranch = pickupBranchObj.BranchName,
                ReturnBranch = returnBranchObj.BranchName,
                PickupDate = PickupDate,
                ReturnDate = ReturnDate
            };

            return View(viewModel);
        }

        // Tarih ve saati birleştiren yardımcı metod (Controller'ın içine ekleyin)
        private static DateTime CombineDateAndTime(string dateString, string timeString)
        {
            // Temanın tarih formatına göre çevirme işlemi
            DateTime.TryParse(dateString, out DateTime date);

            if (string.IsNullOrWhiteSpace(timeString))
                return date;

            var parts = timeString.Split(':');
            var hour = int.Parse(parts[0]);
            var minute = int.Parse(parts[1]);

            return new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
        }
        //public async Task<IActionResult> ListAllCars()
        //{
        //    var cars = await _carService.TGetListAsync();
        //    return View(cars);
        //}

        public async Task<IActionResult> ListAllCars([FromQuery] CarFilterResultViewModel carFilter)
        {
            var cars = await _carService.TGetFilteredCars(carFilter.CategoryId, carFilter.SeatCount, carFilter.FuelType, carFilter.MinPrice, carFilter.MaxPrice);
            var categories = await _categoryService.TGetCategoriesWithCars();

            carFilter.Cars = cars;
            carFilter.Categories = categories;
            //digerler proplar model binding ile otomatik atanıyor dolu geliyor. 
            //bunları db den çekip geçtik.

            return View(carFilter);
        }

        


    }
}
