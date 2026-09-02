using Microsoft.AspNetCore.Mvc;
using RentalyBooking.BusinessLayer.Abstract;

namespace RentalyBooking.WebUI.ViewComponents.ProcessViewComponent
{
    public class ProcessViewComponentPartial : ViewComponent
    {
        private readonly IProcessService _processService;

        public ProcessViewComponentPartial(IProcessService processService)
        {
            _processService = processService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var process = await _processService.TGetListAsync();
            return View(process);
        }
    }
}
