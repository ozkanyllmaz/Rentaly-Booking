using RentalyBooking.EntityLayer.Entities;
using RentalyBooking.EntityLayer.Enums;

namespace RentalyBooking.WebUI.Models
{
    public class CarFilterResultViewModel
    {
        public List<Car> Cars { get; set; }
        public List<Category> Categories { get; set; }


        public int? CategoryId { get; set; }
        public int? SeatCount {  get; set; }
        public List<FuelType>? FuelType { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

    }
}
