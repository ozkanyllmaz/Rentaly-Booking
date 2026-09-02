using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.EntityLayer.Entities;
using System.Net;
using System.Net.Mail;

namespace RentalyBooking.WebUI.Areas.Admin.Controllers
{
    public class RentalyController : BaseController
    {
        private readonly IRentalyService _rentalyService;
        private readonly ICustomerService _customerService;
        private readonly IBranchService _branchService;
        private readonly ICarService _carService;
        private readonly IBrandService _brandService;
        private readonly ICouponService _couponService;

        public RentalyController(IRentalyService rentalyService, ICustomerService customerService, IBranchService branchService, ICarService carService, IBrandService brandService, ICouponService couponService)
        {
            _rentalyService = rentalyService;
            _customerService = customerService;
            _branchService = branchService;
            _carService = carService;
            _brandService = brandService;
            _couponService = couponService;
        }

        public async Task<IActionResult> RentalyList()
        {
            var values = await _rentalyService.TGetRentalyWithAllFeatures();
            return View(values);
        }

        public async Task<IActionResult> DeleteRentaly(int id)
        {
            await _rentalyService.TDeleteAsync(id);
            return RedirectToAction("RentalyList");
        }

        [HttpGet]
        public async Task<IActionResult> CreateRentaly()
        {
            var customers = await _customerService.TGetListAsync();
            var customerValues = customers.Select(x => new
            {
                CustomerId = x.CustomerId,
                FullName = $"{x.Name} {x.Surname}"
            }).ToList();

            ViewBag.Customers = new SelectList(customerValues, "CustomerId", "FullName");
            ViewBag.Cars = new SelectList(await _carService.TGetListAsync(), "CarId", "PlateNumber");
            ViewBag.Branches = new SelectList(await _branchService.TGetListAsync(), "BranchId", "BranchName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRentaly(Rentaly entity)
        {
            entity.Status = "Onay Bekliyor";
            await _rentalyService.TInsertAsync(entity);
            return RedirectToAction("RentalyList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRentaly(int id)
        {
            var customers = await _customerService.TGetListAsync();
            var customerValues = customers.Select(x => new
            {
                CustomerId = x.CustomerId,
                FullName = $"{x.Name} {x.Surname}"
            }).ToList();

            ViewBag.Customers = new SelectList(customerValues, "CustomerId", "FullName");
            ViewBag.Cars = new SelectList(await _carService.TGetListAsync(), "CarId", "PlateNumber");
            ViewBag.Branches = new SelectList(await _branchService.TGetListAsync(), "BranchId", "BranchName");

            var value = await _rentalyService.TGetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRentaly(Rentaly entity)
        {
            await _rentalyService.TUpdateAsync(entity);
            return RedirectToAction("RentalyList");
        }

        [HttpGet]
        public async Task<IActionResult> GetRentalyById(int id)
        {
            var value = await _rentalyService.TGetByIdAsync(id);
            return View(value);
        }

        public async Task<IActionResult> ApproveRental(int id)
        {
            var result = await _rentalyService.TUpdateStatus(id, "Onaylandı");
            if (result > 0)
            {
                var allRentals = await _rentalyService.TGetRentalyWithAllFeatures();
                var rentalInfo = allRentals.FirstOrDefault(x => x.RentalyId == id);

                if (rentalInfo != null && rentalInfo.Customer != null && rentalInfo.Car != null)
                {
                    try
                    {
                        string generatedCoupon = $"RENTALY-{Guid.NewGuid().ToString().Substring(0, 5).ToUpper()}";

                        var newCoupon = new Coupon
                        {
                            Code = generatedCoupon,
                            DiscountPercentage = 20, // %20 İndirim
                            IsUsed = false,
                            ExpirationDate = DateTime.Now.AddMonths(6), // 6 Ay geçerlilik süresi
                            CustomerId = rentalInfo.CustomerId
                        };
                        await _couponService.TInsertAsync(newCoupon);

                        string carFullName = $"{rentalInfo.Car.Brand?.BrandName} {rentalInfo.Car.CarModel?.ModelName}";

                        string pickupStr = rentalInfo.PickupDate.ToString("dd.MM.yyyy HH:mm");
                        string returnStr = rentalInfo.ReturnDate.ToString("dd.MM.yyyy HH:mm");

                        await SendApprovalEmailAsync(
                            rentalInfo.Customer.Email,
                            rentalInfo.Customer.Name,
                            carFullName,
                            rentalInfo.Car.PlateNumber,                 
                            rentalInfo.PickupBranch?.BranchName ?? "-", 
                            rentalInfo.ReturnBranch?.BranchName ?? "-", 
                            pickupStr,
                            returnStr,
                            generatedCoupon
                        );

                    }
                    catch (Exception ex)
                    {
                        TempData["Success"] = "Kiralama onaylandı ancak e-posta gönderilirken bir hata oluştu: " + ex.Message;
                    }
                }

                TempData["Success"] = "Kiralama başarıyla onaylandı.";
            }
            else
            {
                TempData["Error"] = "İşlem sırasında bir hata oluştu veya kiralama bulunamadı.";
            }

            return RedirectToAction("RentalyList");
        }

        public async Task<IActionResult> RejectRental(int id)
        {
            var result = await _rentalyService.TUpdateStatus(id, "İptal");
            if (result > 0)
            {
                TempData["Success"] = "Kiralama iptal edildi.";
            }
            else
            {
                TempData["Error"] = "İşlem sırasında bir hata oluştu veya kiralama bulunamadı.";
            }
            return RedirectToAction("RentalyList");
        }

        public async Task<IActionResult> CompleteRental(int id)
        {
            // Durumu "Tamamlandı" yapıyoruz. Dal içindeki yazdığımız kod 
            // bunu algılayıp arabanın şubesini otomatik değiştirecek.
            var result = await _rentalyService.TUpdateStatus(id, "Tamamlandı");

            if (result > 0)
            {
                TempData["Success"] = "Araç teslim alındı. Aracın güncel şubesi bırakılan şube olarak güncellendi.";
            }
            else
            {
                TempData["Error"] = "İşlem sırasında bir hata oluştu veya kiralama bulunamadı.";
            }

            return RedirectToAction("RentalyList");
        }

        private async Task SendApprovalEmailAsync(string toEmail, string customerName, string carName, string plateNumber, string pickupBranch, string returnBranch, string pickupDate, string returnDate, string couponCode)
        {
            var mailMessage = new MailMessage();
            // NOT: From adresi ile Credentials adresinizin aynı olması spama düşmeyi engeller.
            mailMessage.From = new MailAddress("ozkanyilmaz.dev@gmail.com", "Rentaly Araç Kiralama");
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = "Rezervasyonunuz Onaylandı & Size Özel İndirim Kuponu!";
            mailMessage.IsBodyHtml = true;

            // Şık ve Kurumsal HTML Mail Şablonu (Yeni alanlar eklendi)
            string mailBody = $@"
    <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #e0e0e0; border-radius: 10px; overflow: hidden; box-shadow: 0 4px 10px rgba(0,0,0,0.05);'>
        <div style='background-color: #1a1a1a; padding: 20px; text-align: center;'>
            <h1 style='color: #ffffff; margin: 0;'>Rentaly</h1>
        </div>
        <div style='padding: 30px; background-color: #ffffff;'>
            <h2 style='color: #4CAF50; margin-top: 0;'>Rezervasyonunuz Onaylandı! 🚗</h2>
            <p style='color: #555; font-size: 16px; line-height: 1.5;'>Merhaba <strong>{customerName}</strong>,</p>
            <p style='color: #555; font-size: 16px; line-height: 1.5;'>Harika haber! Yaptığınız rezervasyon başarıyla onaylanmıştır. Kiralama özetiniz aşağıdadır:</p>
            
            <table style='width: 100%; margin: 20px 0; border-collapse: collapse; font-size: 15px;'>
                <tr>
                    <td style='padding: 12px 10px; border-bottom: 1px solid #eee; color: #777; width: 40%;'>Araç Bilgisi:</td>
                    <td style='padding: 12px 10px; border-bottom: 1px solid #eee; font-weight: bold; color: #333;'>{carName} (Plaka: {plateNumber})</td>
                </tr>
                <tr>
                    <td style='padding: 12px 10px; border-bottom: 1px solid #eee; color: #777;'>Alış Noktası:</td>
                    <td style='padding: 12px 10px; border-bottom: 1px solid #eee; font-weight: bold; color: #333;'>{pickupBranch}</td>
                </tr>
                <tr>
                    <td style='padding: 12px 10px; border-bottom: 1px solid #eee; color: #777;'>Alış Tarihi:</td>
                    <td style='padding: 12px 10px; border-bottom: 1px solid #eee; font-weight: bold; color: #333;'>{pickupDate}</td>
                </tr>
                <tr>
                    <td style='padding: 12px 10px; border-bottom: 1px solid #eee; color: #777;'>Bırakış Noktası:</td>
                    <td style='padding: 12px 10px; border-bottom: 1px solid #eee; font-weight: bold; color: #333;'>{returnBranch}</td>
                </tr>
                <tr>
                    <td style='padding: 12px 10px; border-bottom: 1px solid #eee; color: #777;'>Dönüş Tarihi:</td>
                    <td style='padding: 12px 10px; border-bottom: 1px solid #eee; font-weight: bold; color: #333;'>{returnDate}</td>
                </tr>
            </table>

            <div style='background: linear-gradient(135deg, #f6d365 0%, #fda085 100%); padding: 25px; border-radius: 8px; text-align: center; margin-top: 30px;'>
                <h3 style='margin: 0; color: #fff; font-size: 20px;'>Bizi Seçtiğiniz İçin Teşekkürler!</h3>
                <p style='color: #fff; font-size: 15px; margin: 10px 0 15px 0;'>Bir sonraki kiralamanızda geçerli <strong>%20 İndirim</strong> kuponunuz:</p>
                <div style='background-color: rgba(255,255,255,0.9); display: inline-block; padding: 10px 25px; border-radius: 5px; font-size: 24px; font-weight: bold; color: #e74c3c; letter-spacing: 2px;'>
                    {couponCode}
                </div>
            </div>
            
            <p style='color: #999; font-size: 13px; margin-top: 30px; text-align: center;'>Bizi tercih ettiğiniz için teşekkür ederiz. İyi yolculuklar!</p>
        </div>
    </div>";

            mailMessage.Body = mailBody;

            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential("<senin_mailin>@gmail.com", "[16 haneli uygulama şifren]");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mailMessage);
            }
        }
    }
}
