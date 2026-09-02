using RentalyBooking.DataAccessLayer.Abstract;
using RentalyBooking.DataAccessLayer.Concrete;
using RentalyBooking.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.DataAccessLayer.EntityFramework
{
    public class EfFuelTypeDal : IFuelTypeDal
    {
        private readonly RentalyContext _context;

        public EfFuelTypeDal(RentalyContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetAllFuelTypes()
        {
            var properties = typeof(FuelPrice).GetProperties();

            var fuelTypes = properties
                .Select(p => p.Name)
                .Where(name => name.EndsWith("Price"))
                .ToList();

            return await Task.FromResult(fuelTypes);
        }
    }
}
