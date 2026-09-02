using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.DataAccessLayer.Abstract
{
    public interface IGenericDal<T>
    {
        Task InsertAsync(T entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null);
        Task<T> GetByIdAsync(int id);
    }
}
