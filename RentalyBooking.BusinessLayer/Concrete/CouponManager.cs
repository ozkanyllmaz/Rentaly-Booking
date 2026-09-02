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
    public class CouponManager : ICouponService
    {
        private readonly ICouponDal _couponDal;

        public CouponManager(ICouponDal couponDal)
        {
            _couponDal = couponDal;
        }

        public async Task TDeleteAsync(int id)
        {
            await _couponDal.DeleteAsync(id);
        }

        public async Task<Coupon> TGetByIdAsync(int id)
        {
            return await _couponDal.GetByIdAsync(id);
        }

        public async Task<List<Coupon>> TGetListAsync()
        {
            return await _couponDal.GetListAsync();
        }

        public async Task TInsertAsync(Coupon entity)
        {
            await _couponDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(Coupon entity)
        {
            await _couponDal.UpdateAsync(entity);
        }
    }
}
