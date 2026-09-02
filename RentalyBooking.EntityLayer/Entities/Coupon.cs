using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.EntityLayer.Entities
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
   
        public string Code { get; set; }

        public int DiscountPercentage { get; set; }

        public bool IsUsed { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
