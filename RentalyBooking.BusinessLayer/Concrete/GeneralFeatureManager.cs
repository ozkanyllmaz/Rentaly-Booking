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
    public class GeneralFeatureManager : IGeneralFeatureService
    {
        private readonly IGeneralFeatureDal _generalFeatureDal;

        public GeneralFeatureManager(IGeneralFeatureDal generalFeatureDal)
        {
            _generalFeatureDal = generalFeatureDal;
        }

        public async Task TDeleteAsync(int id)
        {
            await _generalFeatureDal.DeleteAsync(id);
        }

        public async Task<GeneralFeature> TGetByIdAsync(int id)
        {
            return await _generalFeatureDal.GetByIdAsync(id);
        }

        public async Task<List<GeneralFeature>> TGetListAsync()
        {
            return await _generalFeatureDal.GetListAsync();
        }

        public async Task TInsertAsync(GeneralFeature entity)
        {
            await _generalFeatureDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(GeneralFeature entity)
        {
            await _generalFeatureDal.UpdateAsync(entity);
        }
    }
}
