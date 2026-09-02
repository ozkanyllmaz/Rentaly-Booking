using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.DataAccessLayer.Abstract;
using RentalyBooking.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.BusinessLayer.Concrete
{
    public class RentalyManager : IRentalyService
    {
        private readonly IRentalyDal _rentalyDal;

        public RentalyManager(IRentalyDal rentalyDal)
        {
            _rentalyDal = rentalyDal;
        }

        public async Task<List<Rentaly>> TGetRentalyWithAllFeatures()
        {
            return await _rentalyDal.GetRentalyWithAllFeaturesAsync();
        }

        public async Task TDeleteAsync(int id)
        {
            await _rentalyDal.DeleteAsync(id);
        }

        public async Task<Rentaly> TGetByIdAsync(int id)
        {
            return await _rentalyDal.GetByIdAsync(id);
        }

        public async Task<List<Rentaly>> TGetListAsync()
        {
            return await _rentalyDal.GetListAsync();
        }

        public async Task TInsertAsync(Rentaly entity)
        {
            await _rentalyDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(Rentaly entity)
        {
            await _rentalyDal.UpdateAsync(entity); 
        }

        public async Task<int> TUpdateStatus(int id, string newStatus)
        {
            return await _rentalyDal.UpdateStatusAsync(id, newStatus);
        }
    }
}
