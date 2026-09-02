using RentalyBooking.EntityLayer.Entities;
using RentalyBooking.EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.DataAccessLayer.Abstract
{
    public interface ICarDal:IGenericDal<Car>
    {
        Task<List<Car>> GetCarsWithBrandsAsync();
        Task<Car> GetCarWithBrandAsync(int id);
        Task<List<Car>> GetCarsByBranchIdAsync(int id);
        Task<List<Car>> GetCarsByCategoryAsync(int id);
        Task<List<Car>> GetAvailableCarsByFilters(int CategoryId, int PickupBranch, int ReturnBranch, DateTime PickupDate, DateTime ReturnDate);
        Task<List<Car>> GetAllCars();
        Task<List<Car>> GetFilteredCars(int? CategoryId, int? Seat, List<FuelType>? FuelType, decimal? MinPrice, decimal? MaxPrice);
    }
}
