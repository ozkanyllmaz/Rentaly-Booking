using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.EntityLayer.Entities
{
    public class OurFeature
    {
        public int OurFeatureId { get; set; }
        public string FeatureName { get; set; }
        public string FeatureDescription { get; set; }
        public string FeatureIcon { get; set; }
        public string ImageUrl { get; set; }
    }
}
