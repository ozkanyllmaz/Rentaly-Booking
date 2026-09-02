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
    public class FAQManager : IFAQService
    {
        private readonly IFAQDal _fAQDal;

        public FAQManager(IFAQDal fAQDal)
        {
            _fAQDal = fAQDal;
        }

        public async Task TDeleteAsync(int id)
        {
            await _fAQDal.DeleteAsync(id);
        }

        public async Task<FAQ> TGetByIdAsync(int id)
        {
            return await _fAQDal.GetByIdAsync(id);
        }

        public async Task<List<FAQ>> TGetListAsync()
        {
            return await _fAQDal.GetListAsync();
        }

        public async Task TInsertAsync(FAQ entity)
        {
            await _fAQDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(FAQ entity)
        {
            await _fAQDal.UpdateAsync(entity);
        }
    }
}
