using Microsoft.AspNetCore.Mvc;
using RentalyBooking.BusinessLayer.Abstract;

namespace RentalyBooking.WebUI.ViewComponents.CarListTopTenViewComponent
{
    public class CarListTopTenViewComponentPartial : ViewComponent
    {
        private readonly ICarService _carService;

        public CarListTopTenViewComponentPartial(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cars = (await _carService.TGetAllCars()).Take(10).ToList();
            return View(cars);
        }
    }
}
