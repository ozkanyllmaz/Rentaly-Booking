using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.BusinessLayer.Abstract
{
    public interface IFuelTypeService
    {
        public Task<List<string>> TGetAllFuelTypes();
    }
}
