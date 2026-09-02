using Microsoft.AspNetCore.Mvc;
using RentalyBooking.BusinessLayer.Abstract;

namespace RentalyBooking.WebUI.ViewComponents.FAQViewComponent
{
    public class FAQViewComponentPartial : ViewComponent
    {
        private readonly IFAQService _faqService;

        public FAQViewComponentPartial(IFAQService faqService)
        {
            _faqService = faqService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _faqService.TGetListAsync();
            return View(values);
        }
    }
}
