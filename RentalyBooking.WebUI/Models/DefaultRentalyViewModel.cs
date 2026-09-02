using RentalyBooking.EntityLayer.Entities;

namespace RentalyBooking.WebUI.Models
{
    public class DefaultRentalyViewModel
    {
        public List<Category> Category { get; set; }
        public List<Branch> Branches { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }



    }
}
