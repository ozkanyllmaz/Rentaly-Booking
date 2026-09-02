using RentalyBooking.EntityLayer.Entities;
using RentalyBooking.EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.BusinessLayer.Abstract
{
    public interface ICarService : IGenericService<Car>
    {
        Task<List<Car>> TGetCarsWithBrands();
        Task<Car> TGetCarWithBrand(int id);
        Task<List<Car>> TGetCarsByBranchId(int id);
        Task<List<Car>> TGetCarsByCategory(int id);

        Task<List<Car>> TGetAvailableCarsByDates(DateTime pickup, DateTime returnDate);

        Task<List<Car>> TGetAvailableCarsByFilters(int CategoryId, int PickupBranch, int ReturnBranch, DateTime PickupDate, DateTime ReturnDate);
        Task<List<Car>> TGetAllCars();
        Task<List<Car>> TGetFilteredCars(int? CategoryId, int? Seat, List<FuelType>? FuelType, decimal? MinPrice, decimal? MaxPrice);
    }
}
