using Microsoft.AspNetCore.Mvc;

namespace RentalyBooking.WebUI.ViewComponents.TestimonialViewComponent
{
    public class TestimonialViewComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
