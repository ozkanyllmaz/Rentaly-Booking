using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.EntityLayer.Entities
{
    public class FuelPrice
    {
        public int FuelPriceID { get; set; }
        public string DistrictName { get; set; }
        public decimal GasolinePrice { get; set; }
        public decimal DieselPrice { get; set; }
        public decimal LpgPrice { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
