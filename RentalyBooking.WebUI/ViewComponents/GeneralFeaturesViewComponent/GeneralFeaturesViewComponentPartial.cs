using Microsoft.AspNetCore.Mvc;
using RentalyBooking.BusinessLayer.Abstract;

namespace RentalyBooking.WebUI.ViewComponents.GeneralFeaturesViewComponent
{
    public class GeneralFeaturesViewComponentPartial : ViewComponent
    {
        private readonly IGeneralFeatureService _generalFeatureService;

        public GeneralFeaturesViewComponentPartial(IGeneralFeatureService generalFeatureService)
        {
            _generalFeatureService = generalFeatureService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _generalFeatureService.TGetListAsync();
            return View(values);  
        }
    }
}
