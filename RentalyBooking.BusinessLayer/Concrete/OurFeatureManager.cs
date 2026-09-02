using RentalyBooking.BusinessLayer.Abstract;
using RentalyBooking.DataAccessLayer.Abstract;
using RentalyBooking.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.BusinessLayer.Concrete
{
    public class OurFeatureManager : IOurFeatureService
    {
        private readonly IOurFeatureDal _ourFeatureDal;

        public OurFeatureManager(IOurFeatureDal ourFeatureDal)
        {
            _ourFeatureDal = ourFeatureDal;
        }

        public Task TDeleteAsync(int id)
        {
            return _ourFeatureDal.DeleteAsync(id);
        }

        public Task<OurFeature> TGetByIdAsync(int id)
        {
            return _ourFeatureDal.GetByIdAsync(id);
        }

        public Task<List<OurFeature>> TGetListAsync()
        {
            return _ourFeatureDal.GetListAsync();
        }

        public Task TInsertAsync(OurFeature entity)
        {
            return _ourFeatureDal.InsertAsync(entity);
        }

        public Task TUpdateAsync(OurFeature entity)
        {
            return _ourFeatureDal.UpdateAsync(entity);
        }
    }
}
