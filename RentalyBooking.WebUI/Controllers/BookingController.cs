using Microsoft.AspNetCore.Mvc;
using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.EntityLayer.Entities;
using RentalyBooking.WebUI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RentalyBooking.WebUI.Controllers
{
    public class BookingController : Controller
    {
        private readonly ICarService _carService;
        private readonly IBranchService _branchService;
        private readonly IRentalyService _rentalyService;
        private readonly ICustomerService _customerService;
        private readonly ICouponService _couponService;

        public BookingController(
            ICarService carService,
            IBranchService branchService,
            IRentalyService rentalyService,
            ICustomerService customerService,
            ICouponService couponService)
        {
            _carService = carService;
            _branchService = branchService;
            _rentalyService = rentalyService;
            _customerService = customerService;
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? carId)
        {
            var cars = await _carService.TGetAllCars();
            var branches = await _branchService.TGetListAsync();

            Console.WriteLine("cars: "+ cars);
            Console.WriteLine("branches: "+ branches);

            var model = new BookingViewModel
            {
                Cars = cars,
                Branches = branches
            };

            if (carId.HasValue)
            {
                // 1. Aracı seçili hale getir
                model.CarId = carId.Value;

                // 2. Seçilen aracı bul ve bulunduğu şubeyi 'Alış Şubesi' olarak işaretle
                var selectedCar = cars.FirstOrDefault(x => x.CarId == carId.Value);
                if (selectedCar != null)
                {
                    model.PickupBranchId = selectedCar.BranchId;
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] BookingViewModel model)
        {
            if (!ModelState.IsValid)
            {

                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    Console.WriteLine("FORM HATASI: " + error);
                }

                model.Cars = await _carService.TGetAllCars();
                model.Branches = await _branchService.TGetListAsync();
                return View(model);
            }

            // Tarih + saat alanlarını birleştiriyoruz (view'da tarih ve saat ayrı select/inputlarda)
            var pickupDateTime = CombineDateAndTime(model.PickupDate, model.PickupTime);
            var returnDateTime = CombineDateAndTime(model.ReturnDate, model.ReturnTime);

            var existingCustomer = (await _customerService.TGetListAsync())
                        .FirstOrDefault(x => x.Email == model.CustomerEmail);

            Customer activeCustomer;

            if (existingCustomer != null)
            {
                // 1. DURUM: Müşteri zaten sistemde var.
                // Formdan gelen yeni bilgilerle eski bilgileri güncelliyoruz (Update)
                existingCustomer.Name = model.CustomerName;
                existingCustomer.Surname = model.CustomerSurname;
                existingCustomer.Phone = model.CustomerPhone;
                existingCustomer.IdentityNumber = model.CustomerIdentityNumber;
                existingCustomer.DrivingLicenseNumber = model.DrivingLicenseNumber;
                existingCustomer.DrivingLicenseDate = model.DrivingLicenseDate;

                await _customerService.TUpdateAsync(existingCustomer);

                // Rezervasyonda kullanılacak müşteri bu
                activeCustomer = existingCustomer;
            }
            else
            {
                // 2. DURUM: Müşteri ilk kez geliyor. Yeni kayıt oluşturuyoruz (Insert)
                var newCustomer = new Customer
                {
                    Name = model.CustomerName,
                    Surname = model.CustomerSurname,
                    Email = model.CustomerEmail,
                    Phone = model.CustomerPhone,
                    IdentityNumber = model.CustomerIdentityNumber,
                    DrivingLicenseNumber = model.DrivingLicenseNumber,
                    DrivingLicenseDate = model.DrivingLicenseDate
                };

                await _customerService.TInsertAsync(newCustomer);

               
                activeCustomer = newCustomer;
            }

            
            var car = (await _carService.TGetAllCars())
                        .FirstOrDefault(c => c.CarId == model.CarId); 

            var days = Math.Max(1, (returnDateTime.Date - pickupDateTime.Date).Days);
            var totalPrice = (car?.DailyPrice ?? 0) * days;

            // --- KUPON KONTROLÜ VE İNDİRİM UYGULAMASI BAŞLANGICI ---
            if (!string.IsNullOrWhiteSpace(model.CouponCode))
            {
                var allCoupons = await _couponService.TGetListAsync();

                // Girilen kodla eşleşen, henüz KULLANILMAMIŞ ve SÜRESİ GEÇMEMİŞ kuponu bul
                var validCoupon = allCoupons.FirstOrDefault(x =>
                    x.Code.ToUpper() == model.CouponCode.ToUpper() &&
                    x.IsUsed == false &&
                    x.ExpirationDate >= DateTime.Now);

                if (validCoupon != null)
                {
                    
                    var couponOwner = await _customerService.TGetByIdAsync(validCoupon.CustomerId);
                    if (couponOwner != null && couponOwner.Email.ToLower() == model.CustomerEmail.ToLower())
                    {
                        // %20 İndirimi Uygula
                        var discountAmount = totalPrice * (validCoupon.DiscountPercentage / 100m);
                        totalPrice -= discountAmount; // Toplam fiyattan düş

                        // Kuponu kullanıldı olarak işaretle ve veritabanını güncelle
                        validCoupon.IsUsed = true;
                        await _couponService.TUpdateAsync(validCoupon);
                    }
                    else
                    {
                        ModelState.AddModelError("CouponCode", "Bu kupon sizin e-posta adresinize ait değil.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("CouponCode", "Girdiğiniz kupon geçersiz, daha önce kullanılmış veya süresi dolmuş.");

                    // Listeleri tekrar doldurup aynı sayfaya geri gönderiyoruz ki hatayı görsün
                    model.Cars = await _carService.TGetAllCars();
                    model.Branches = await _branchService.TGetListAsync();
                    return View(model);
                }
            }

            var rentaly = new Rentaly
            {
                CarId = model.CarId,
                CustomerId = activeCustomer.CustomerId, 
                PickupBranchId = model.PickupBranchId,
                ReturnBranchId = model.ReturnBranchId,
                PickupDate = pickupDateTime,
                ReturnDate = returnDateTime,
                TotalPrice = totalPrice,
                Status = "Onay Bekliyor"
            };

            await _rentalyService.TInsertAsync(rentaly);

            string reservationNo = $"RNT{DateTime.Now.Year}{rentaly.RentalyId:D6}";

            TempData["BookingSuccess"] = true;
            return RedirectToAction("Success", new { reservationNumber = reservationNo });
        }

        private static DateTime CombineDateAndTime(DateTime date, string time)
        {
            if (string.IsNullOrWhiteSpace(time))
                return date;

            var parts = time.Split(':');
            var hour = int.Parse(parts[0]);
            var minute = int.Parse(parts[1]);

            return new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
        }

        [HttpGet]
        public IActionResult Success(string reservationNumber)
        {
            // URL'den gelen rezervasyon numarasını View'a taşıyoruz
            ViewBag.ReservationNo = reservationNumber;
            return View();
        }
    }
}