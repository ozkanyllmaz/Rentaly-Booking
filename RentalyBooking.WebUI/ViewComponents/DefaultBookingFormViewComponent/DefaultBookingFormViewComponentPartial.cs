using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.WebUI.Models;

namespace RentalyBooking.WebUI.ViewComponents
{
    public class DefaultBookingFormViewComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IBranchService _branchService;

        public DefaultBookingFormViewComponentPartial(ICategoryService categoryService, IBranchService branchService)
        {
            _categoryService = categoryService;
            _branchService = branchService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int CategoryId, string PickupBranch, string ReturnBranch, DateTime PickupDate, DateTime ReturnDate)
        {
            var categories = await _categoryService.TGetListAsync();
            var branches = await _branchService.TGetListAsync();


            var rentalViewModel = new DefaultRentalyViewModel
            {
                Category = categories,
                Branches = branches,
                PickupDate = PickupDate,
                ReturnDate = ReturnDate
            };

            
            return View(rentalViewModel);
        }
    }
}
