using RentalyBooking.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalyBooking.WebUI.Models
{
    public class BookingViewModel
    {
        // Select listelerini doldurmak için
        public List<Car>? Cars { get; set; }
        public List<Branch>? Branches { get; set; }

        // Formdan gelen alanlar
        [Required(ErrorMessage = "Lütfen bir araç seçiniz.")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Lütfen alış şubesi seçiniz.")]
        public int PickupBranchId { get; set; }

        [Required(ErrorMessage = "Lütfen teslim şubesi seçiniz.")]
        public int ReturnBranchId { get; set; }

        [Required(ErrorMessage = "Lütfen alış tarihi giriniz.")]
        [DataType(DataType.Date)]
        public DateTime PickupDate { get; set; }

        public string PickupTime { get; set; }

        [Required(ErrorMessage = "Lütfen teslim tarihi giriniz.")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        public string ReturnTime { get; set; }

        [Required(ErrorMessage = "Adınızı giriniz.")]
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerIdentityNumber { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public DateTime DrivingLicenseDate { get; set; }


        [Required(ErrorMessage = "E-posta adresinizi giriniz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "Telefon numaranızı giriniz.")]
        public string CustomerPhone { get; set; }

        public string? Message { get; set; }

        public string? CouponCode { get; set; }
    }
}