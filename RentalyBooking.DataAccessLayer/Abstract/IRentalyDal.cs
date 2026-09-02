using RentalyBooking.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.DataAccessLayer.Abstract
{
    public interface IRentalyDal:IGenericDal<Rentaly>
    {
        Task<List<Rentaly>> GetRentalyWithAllFeaturesAsync();
        Task<int> UpdateStatusAsync(int id, string newStatus);
    }
}
