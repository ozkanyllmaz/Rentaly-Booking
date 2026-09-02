using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.DataAccessLayer.Abstract;
using RentalyBooking.EntityLayer.Entities;
using RentalyBooking.EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.BusinessLayer.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public async Task<List<Car>> TGetAvailableCarsByDates(DateTime pickup, DateTime returnDate)
        {
            var allIsAvailableCar = await _carDal.GetListAsync(x => 
            x.IsAvailable &&
            x.IsActive &&
            !x.Rentals.Any(r => r.Status != "İptal" && r.PickupDate < returnDate && r.ReturnDate > pickup));

            return allIsAvailableCar;
 
        }

        public async Task TDeleteAsync(int id)
        {
            await _carDal.DeleteAsync(id);
        }

        public async Task<Car> TGetByIdAsync(int id)
        {
            return await _carDal.GetByIdAsync(id);
        }

        public async Task<List<Car>> TGetCarsByBranchId(int id)
        {
            return await _carDal.GetCarsByBranchIdAsync(id);
        }

        public async Task<List<Car>> TGetCarsByCategory(int id)
        {
            return await _carDal.GetCarsByCategoryAsync(id);
        }

        public async Task<List<Car>> TGetCarsWithBrands()
        {
            return await _carDal.GetCarsWithBrandsAsync();
        }

        public async Task<Car> TGetCarWithBrand(int id)
        {
            return await _carDal.GetCarWithBrandAsync(id);
        }

        public async Task<List<Car>> TGetListAsync()
        {
            return await _carDal.GetListAsync();
        }

        public async Task TInsertAsync(Car entity)
        {
            await _carDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(Car entity)
        {
            await _carDal.UpdateAsync(entity);
        }

        public async Task<List<Car>> TGetAvailableCarsByFilters(int CategoryId, int PickupBranch, int ReturnBranch, DateTime PickupDate, DateTime ReturnDate)
        {
            return await _carDal.GetAvailableCarsByFilters(CategoryId, PickupBranch, ReturnBranch, PickupDate, ReturnDate);
        }

        public async Task<List<Car>> TGetAllCars()
        {
            return await _carDal.GetAllCars();
        }

        public async Task<List<Car>> TGetFilteredCars(int? CategoryId, int? Seat, List<FuelType>? FuelType, decimal? MinPrice, decimal? MaxPrice)
        {
            return await _carDal.GetFilteredCars(CategoryId, Seat, FuelType, MinPrice, MaxPrice);
        }
    }
}
