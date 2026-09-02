using Microsoft.EntityFrameworkCore;
using RentalyBooking.DataAccessLayer.Abstract;
using RentalyBooking.DataAccessLayer.Concrete;
using RentalyBooking.DataAccessLayer.RepositoryDesignPattern;
using RentalyBooking.EntityLayer.Entities;
using RentalyBooking.EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.DataAccessLayer.EntityFramework
{
    public class EfCarDal : GenericRepository<Car>, ICarDal
    {
        private readonly RentalyContext _context;
        public EfCarDal(RentalyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetFilteredCars(int? CategoryId, int? Seat, List<FuelType>? FuelType, decimal? MinPrice, decimal? MaxPrice)
        {
            var query = _context.Cars
                 .Include(x => x.CarModel)
                 .Include(x => x.Brand)
                 .Include(x => x.Category)
                 .Include(x => x.Branch)
                 .AsQueryable();

            //kullanıcı kategori seçtiyse bunu sorguya ekle
            if (CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == CategoryId.Value);   
            }
            if (Seat.HasValue)
            {
                query = query.Where(x => x.SeatCount == Seat.Value);
            }
            if (FuelType?.Count() > 0)
            {
                query = query.Where(x => FuelType.Contains(x.FuelType));
            }
            if (MinPrice.HasValue)
            {
                query = query.Where(x => x.DailyPrice >= MinPrice.Value);
            }
            if (MaxPrice.HasValue)
            {
                query = query.Where(x => x.DailyPrice <= MaxPrice.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Car>> GetAllCars()
        {
            return await _context.Cars
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.CarModel)
                .Include(x => x.Branch)
                .ToListAsync();
        }

        public async Task<List<Car>> GetAvailableCarsByFilters(int CategoryId, int PickupBranch, int ReturnBranch, DateTime PickupDate, DateTime ReturnDate)
        {
            return await _context.Cars
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.CarModel)
                .Include(x => x.Branch)
                // 1. KURAL: Araç genel olarak aktif ve kiralanabilir (serviste değil) olmalı
                .Where(x => x.CategoryId == CategoryId &&
                            x.BranchId == PickupBranch &&
                            x.IsActive == true &&
                            x.IsAvailable == true)
                // 2. KURAL: Tarih çakışması kontrolü (Sadece İptal/Red edilmemiş rezervasyonlar için)
                .Where(x => x.Rentals.All(r =>
                    r.Status == "Cancelled" ||
                    r.Status == "Rejected" ||
                    r.ReturnDate <= PickupDate ||
                    r.PickupDate >= ReturnDate))
                .ToListAsync();
        }

        public async Task<List<Car>> GetCarsByBranchIdAsync(int id)
        {
            return await _context.Cars
                .Include(x => x.Brand)
                .Include(x => x.CarModel)
                .Where(x => x.BranchId == id)
                .ToListAsync();
        }

        public async Task<List<Car>> GetCarsByCategoryAsync(int id)
        {
            return await _context.Cars
                .Include(x => x.Brand)
                .Include(x => x.CarModel)
                .Include(x => x.Branch)
                .Include(x => x.Category)
                .Where(x => x.CategoryId == id)
                .ToListAsync();
        }

        public async Task<List<Car>> GetCarsWithBrandsAsync()
        {
            return await _context.Cars
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Include(x => x.CarModel)
                .ToListAsync();
        }

        public async Task<Car> GetCarWithBrandAsync(int id)
        {
            return await _context.Cars
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Include(x => x.CarModel)
                .Include(x => x.Branch)
                .FirstOrDefaultAsync(x => x.CarId == id);
        }
    }
}
