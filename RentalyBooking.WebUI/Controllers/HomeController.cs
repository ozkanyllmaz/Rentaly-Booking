using Microsoft.AspNetCore.Mvc;

namespace RentalyBooking.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
