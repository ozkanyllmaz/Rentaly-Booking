using RentalyBooking.EntityLayer.Entities;

namespace RentalyBooking.WebUI.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public decimal TotalRevenue { get; set; }
        public int TotalCars { get; set; }
        public int ActiveRentedCars { get; set; }
        public int AvailableCars { get; set; }

        public int TotalCustomers { get; set; }
        public int PendingRentals { get; set; }

        public int TotalBranches { get; set; }
        public int CompletedRentals { get; set; }

        public string GasolinePrice { get; set; }
        public string DieselPrice { get; set; }
        public string LpgPrice { get; set; }

        public Dictionary<string, int> CategoryCarCounts { get; set; } = new Dictionary<string, int>();

        public List<Rentaly> RecentRentals { get; set; }

        public string FuelPriceText { get; set; } // RapidAPI'den gelecek metin
    }
}
