using RentalyBooking.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.BusinessLayer.Abstract
{
    public interface IRentalyService : IGenericService<Rentaly>
    {
        Task<List<Rentaly>> TGetRentalyWithAllFeatures();

        Task<int> TUpdateStatus(int id, string newStatus);
    }
}
