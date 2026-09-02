using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.DataAccessLayer.Abstract;
using RentalyBooking.DtoLayer.Dtos;
using RentalyBooking.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.BusinessLayer.Concrete
{
    public class FuelPriceManager : IFuelPriceService
    {
        private readonly IFuelPriceServiceDal _fuelPriceServiceDal;

        public FuelPriceManager(IFuelPriceServiceDal fuelPriceServiceDal, IFuelTypeDal fuelTypeDal)
        {
            _fuelPriceServiceDal = fuelPriceServiceDal;
        }

        public async Task<List<FuelPriceDto>> GetCurrentPricesAsync(string city, string district)
        {
            if (!string.IsNullOrEmpty(district))
            {
                district = district.ToUpper();
            }

            return await _fuelPriceServiceDal.GetCurrentPricesAsync(city, district);
        }


        public async Task<FuelPrice> TGetLastPriceAsync()
        {
            return await _fuelPriceServiceDal.GetLastPriceAsync();
        }

        public async Task TInsertAsync(FuelPrice fuelPrice)
        {
            await _fuelPriceServiceDal.InsertAsync(fuelPrice);
        }
    }
}
