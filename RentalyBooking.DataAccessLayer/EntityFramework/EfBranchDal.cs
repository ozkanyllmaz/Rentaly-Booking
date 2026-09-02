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
    public class EfBranchDal : GenericRepository<Branch>, IBranchDal
    {
        private readonly RentalyContext _context;
        public EfBranchDal(RentalyContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Branch>> GetBranchesWithCarAsync()
        {
            return _context.Branches
                .Include(x => x.Cars)
                .ToListAsync();
        }
    }
}
