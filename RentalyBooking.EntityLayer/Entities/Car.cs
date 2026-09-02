using RentalyBooking.EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.EntityLayer.Entities
{
    public class Car
    {
        public int CarId { get; set; }
        public string PlateNumber { get; set; }
        public string VIN { get; set; } // Şasi No
        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
        public int CarModelId { get; set; }

        [ForeignKey("CarModelId")]
        public CarModel CarModel { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [NotMapped]
        public string CarFullName => $"{Brand?.BrandName} {CarModel?.ModelName}";

        public int BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
        public int Year { get; set; }
        public int Kilometer { get; set; }
        public decimal DailyPrice { get; set; }
        public decimal DepositAmount { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public int SeatCount { get; set; }
        public int LuggageCount { get; set; }
        public FuelType FuelType { get; set; }
        public int DoorCount { get; set; }
        public ICollection<Rentaly> Rentals { get; set; } = new List<Rentaly>();
    }
}
