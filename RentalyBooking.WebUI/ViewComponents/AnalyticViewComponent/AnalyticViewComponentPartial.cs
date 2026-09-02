using Microsoft.AspNetCore.Mvc;
using RentalyBooking.BusinessLayer.Abstract;

namespace RentalyBooking.WebUI.ViewComponents.AnalyticViewComponent
{
    public class AnalyticViewComponentPartial : ViewComponent
    {
        private readonly IRentalyService _rentalService;
        private readonly ICarService _carService;
        private readonly ICustomerService _customerService;
        private readonly IBranchService _branchService;
        public AnalyticViewComponentPartial(IRentalyService rentalService, ICarService carService, ICustomerService customerService, IBranchService branchService)
        {
            _rentalService = rentalService;
            _carService = carService;
            _customerService = customerService;
            _branchService = branchService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var totalRental = (await _rentalService.TGetListAsync()).Count();
            var totalCar = (await _carService.TGetListAsync()).Count();
            var totalCustomer = (await _customerService.TGetListAsync()).Count();
            var totalBranch = (await _branchService.TGetListAsync()).Count();

            ViewBag.TotalRental = totalRental;
            ViewBag.TotalCar = totalCar;
            ViewBag.TotalCustomer = totalCustomer;
            ViewBag.TotalBranch = totalBranch;

            return View();
        }
    }
}
