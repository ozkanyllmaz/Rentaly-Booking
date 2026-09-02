using RentalyBooking.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task TDeleteAsync(int id);
        Task TInsertAsync(T entity);
        Task<List<T>> TGetListAsync();
        Task TUpdateAsync(T entity);
        Task<T> TGetByIdAsync(int id);
    }
}
