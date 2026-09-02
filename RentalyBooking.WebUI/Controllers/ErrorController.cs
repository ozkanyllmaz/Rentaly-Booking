using Microsoft.AspNetCore.Mvc;

namespace RentalyBooking.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/Error404")]
        public IActionResult PageNotFound(int code)
        {
            if(code == 404)
            {
                return View("Error404");
            }

            return View("DefaultError");
        }
    }
}
