using RentalyBooking.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.DataAccessLayer.Abstract
{
    public interface ICarModelDal:IGenericDal<CarModel>
    {
        Task<List<CarModel>> GetCarModelsByBrandAsync(int brandId);
        Task<List<CarModel>> GetCarModelsWithBrandAsync();
    }
}
