using Microsoft.AspNetCore.Mvc;
using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.EntityLayer.Entities;

namespace RentalyBooking.WebUI.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> CustomerList()
        {
            var values = await _customerService.TGetListAsync();
            return View(values);
        }

        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.TDeleteAsync(id);
            return RedirectToAction("CustomerList");
        }

        [HttpGet]
        public async Task<IActionResult> CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer(Customer entity)
        {
            await _customerService.TInsertAsync(entity);
            return RedirectToAction("CustomerList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var value = await _customerService.TGetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCustomer(Customer entity)
        {
            await _customerService.TUpdateAsync(entity);
            return RedirectToAction("CustomerList");
        }

        public async Task<IActionResult> GetCustomerById(int id)
        {
            var value = await _customerService.TGetByIdAsync(id);
            return View(value);
        }
    }
}
