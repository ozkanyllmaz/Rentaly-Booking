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
    public class EfRentalyDal : GenericRepository<Rentaly>, IRentalyDal
    {
        private readonly RentalyContext _context;
        public EfRentalyDal(RentalyContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Rentaly>> GetRentalyWithAllFeaturesAsync()
        {
            return _context.Rentals
                .Include(x => x.Car)
                .Include(x => x.Customer)
                .Include(x => x.PickupBranch)
                .Include(x => x.ReturnBranch)
                .OrderByDescending(x => x.PickupDate)
                .ToListAsync();
        }

        public async Task<int> UpdateStatusAsync(int id, string newStatus)
        {
            // 1. Aracı (Car) da Include ederek çekiyoruz ki şubesini güncelleyebilelim.
            var rental = await _context.Rentals
                .Include(x => x.Car)
                .Where(x => x.RentalyId == id)
                .FirstOrDefaultAsync();

            if (rental == null)
            {
                return 0;
            }

            // 2. Kiralama statüsünü güncelliyoruz
            rental.Status = newStatus;

            // 3. EĞER kiralama tamamlandıysa/araç teslim edildiyse arabanın şubesini değiştir
            if (newStatus == "Tamamlandı")
            {
                if (rental.Car != null)
                {
                    rental.Car.BranchId = rental.ReturnBranchId;
                }
            }

            return await _context.SaveChangesAsync();
        }
    }
}
