using Microsoft.AspNetCore.Mvc;
using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.WebUI.Areas.Admin.Models;
using System.Threading.Tasks;

namespace RentalyBooking.WebUI.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly ICarService _carService;
        private readonly IRentalyService _rentalyService;
        private readonly ICustomerService _customerService;
        private readonly IBranchService _branchService;
        private readonly IFuelPriceService _fuelPriceService;
        private readonly ICategoryService _categoryService;

        public DashboardController(ICarService carService, IRentalyService rentalyService, ICustomerService customerService, IBranchService branchService, IFuelPriceService fuelPriceService, ICategoryService categoryService)
        {
            _carService = carService;
            _rentalyService = rentalyService;
            _customerService = customerService;
            _branchService = branchService;
            _fuelPriceService = fuelPriceService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel();

            var allCars = await _carService.TGetListAsync();
            var allRentals = await _rentalyService.TGetListAsync();
            var allCustomer = await _customerService.TGetListAsync();
            var allBranch = await _branchService.TGetListAsync();

            model.TotalRevenue = allRentals.Where(x => x.Status == "Tamamlandı").Sum(x => x.TotalPrice);
            model.TotalCars = allCars.Count();
            model.AvailableCars = allCars.Where(x => x.IsAvailable == true).Count();
            model.ActiveRentedCars = allRentals.Where(x => x.Status == "Aktif").Count();
            model.TotalCustomers = allCustomer.Count();
            model.PendingRentals = allRentals.Where(x => x.Status == "Beklemede").Count();
            model.TotalBranches = allBranch.Count();
            model.CompletedRentals = allRentals.Count(x => x.Status != null & x.Status.Trim().ToLower() == "tamamlandı");
            model.RecentRentals = allRentals.OrderByDescending(x => x.RentalyId)
                .Take(5)
                .ToList();

            var localPrice = await _fuelPriceService.TGetLastPriceAsync();

            if (localPrice != null)
            {
                model.GasolinePrice = $"{localPrice.GasolinePrice} ₺";
                model.DieselPrice = $"{localPrice.DieselPrice} ₺";
                model.LpgPrice = $"{localPrice.LpgPrice} ₺";

            }
            else
            {
                model.FuelPriceText = "Fiyat bilgisi şu an veritabanında mevcut değil.";
            }

            var categories = await _categoryService.TGetCategoriesWithCars();

            model.CategoryCarCounts = categories.ToDictionary(
                x => x.CategoryName,
                x => x.Cars != null ? x.Cars.Count : 0
            );

            return View(model);
        }
    }
}
