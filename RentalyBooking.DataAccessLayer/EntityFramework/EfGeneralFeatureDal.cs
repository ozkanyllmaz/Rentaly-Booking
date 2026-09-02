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
    public class EfGeneralFeatureDal : GenericRepository<GeneralFeature>, IGeneralFeatureDal
    {
        public EfGeneralFeatureDal(RentalyContext context) : base(context)
        {
        }
    }
}
