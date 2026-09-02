using RentalyBooking.DtoLayer.Dtos;
using RentalyBooking.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.BusinessLayer.Abstract
{
    public interface IFuelPriceService
    {
        Task<List<FuelPriceDto>> GetCurrentPricesAsync(string city, string district);

        Task TInsertAsync(FuelPrice fuelPrice);
        Task<FuelPrice> TGetLastPriceAsync();

    }
}
