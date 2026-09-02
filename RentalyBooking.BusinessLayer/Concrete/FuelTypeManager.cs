using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.BusinessLayer.Concrete
{
    public class FuelTypeManager : IFuelTypeService
    {
        private readonly IFuelTypeDal _fuelTypeDal;

        public FuelTypeManager(IFuelTypeDal fuelTypeDal)
        {
            _fuelTypeDal = fuelTypeDal;
        }

        public async Task<List<string>> TGetAllFuelTypes()
        {
            return await _fuelTypeDal.GetAllFuelTypes();
        }
    }
}
