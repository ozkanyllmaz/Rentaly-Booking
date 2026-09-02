using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.EntityLayer.Entities
{
    public class Rentaly
    {
        public int RentalyId { get; set; }
        public int CarId { get; set; }

        [ForeignKey("CarId")]
        public Car Car { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer {  get; set; }
        public int PickupBranchId { get; set; }

        [ForeignKey("PickupBranchId")]
        public Branch PickupBranch { get; set; }
        public int ReturnBranchId { get; set; }

        [ForeignKey("ReturnBranchId")]
        public Branch ReturnBranch { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

    }
}
