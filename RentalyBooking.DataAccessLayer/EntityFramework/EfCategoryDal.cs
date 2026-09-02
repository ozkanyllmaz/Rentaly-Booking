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
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        private readonly RentalyContext _context;
        public EfCategoryDal(RentalyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesWithCarsAsync()
        {
            return await _context.Categories
                .Include(x => x.Cars)
                .ToListAsync();
        }
    }
}
