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
    public class ProcessManager : IProcessService
    {
        private readonly IProcessDal _processDal;

        public ProcessManager(IProcessDal processDal)
        {
            _processDal = processDal;
        }

        public async Task TDeleteAsync(int id)
        {
            await _processDal.DeleteAsync(id);
        }

        public async Task<Process> TGetByIdAsync(int id)
        {
            return await _processDal.GetByIdAsync(id);
        }

        public async Task<List<Process>> TGetListAsync()
        {
            return await _processDal.GetListAsync();
        }

        public async Task TInsertAsync(Process entity)
        {
            await _processDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(Process entity)
        {
            await _processDal.UpdateAsync(entity);
        }
    }
}
