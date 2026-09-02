using Microsoft.EntityFrameworkCore;
using RentalyBooking.DataAccessLayer.Abstract;
using RentalyBooking.DataAccessLayer.Concrete;
using RentalyBooking.DataAccessLayer.RepositoryDesignPattern;
using RentalyBooking.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.DataAccessLayer.EntityFramework
{
    public class EfCarModelDal : GenericRepository<CarModel>, ICarModelDal
    {
        private readonly RentalyContext _context;
        public EfCarModelDal(RentalyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<CarModel>> GetCarModelsByBrandAsync(int brandId)
        {
            return await _context.CarModels.Where(x => x.BrandId == brandId).ToListAsync(); 
        }

        public async Task<List<CarModel>> GetCarModelsWithBrandAsync()
        {
            return await _context.CarModels
                .Include(x => x.Brand)
                .OrderBy(x => x.Brand.BrandName)
                .ToListAsync();
        }
    }
}
