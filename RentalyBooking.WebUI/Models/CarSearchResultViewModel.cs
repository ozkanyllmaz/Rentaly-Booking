using RentalyBooking.EntityLayer.Entities;

namespace RentalyBooking.WebUI.Models
{
    public class CarSearchResultViewModel
    {
        public List<Car> Cars { get; set; }
        
        public string CategoryName { get; set; }
        public string PickupBranch { get; set; }
        public string ReturnBranch { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
