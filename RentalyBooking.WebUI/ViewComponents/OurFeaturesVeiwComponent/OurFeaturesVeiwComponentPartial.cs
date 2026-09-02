using Microsoft.AspNetCore.Mvc;
using RentalyBooking.BusinessLayer.Abstract;

namespace RentalyBooking.WebUI.ViewComponents
{
    public class OurFeaturesVeiwComponentPartial : ViewComponent
    {
        private readonly IOurFeatureService _ourFeatureService;

        public OurFeaturesVeiwComponentPartial(IOurFeatureService ourFeatureService)
        {
            _ourFeatureService = ourFeatureService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var features = await _ourFeatureService.TGetListAsync();

            return View(features);
        }
    }
}
