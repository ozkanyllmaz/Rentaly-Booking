using Microsoft.AspNetCore.Mvc;
using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.EntityLayer.Entities;
using System.Threading.Tasks;

namespace RentalyBooking.WebUI.Areas.Admin.Controllers
{
    public class BranchController : BaseController
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        public async Task<IActionResult> BranchList()
        {
            var values = await _branchService.TGetBranchesWithCar();
            return View(values);
        }
        public async Task<IActionResult> DeleteBranch(int id)
        {
            await _branchService.TDeleteAsync(id);
            return RedirectToAction("BranchList");
        }

        [HttpGet]
        public async Task<IActionResult> CreateBranch()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBranch(Branch entity)
        {
            await _branchService.TInsertAsync(entity);
            return RedirectToAction("BranchList");
        }

        public async Task<IActionResult> GetBranchById(int id)
        {
            var value = await _branchService.TGetByIdAsync(id);
            return View(value);    
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBranch(int id)
        {
            var value = await _branchService.TGetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBranch(Branch entity)
        {
            await _branchService.TUpdateAsync(entity);
            return RedirectToAction("BranchList");
        }
    }
}
