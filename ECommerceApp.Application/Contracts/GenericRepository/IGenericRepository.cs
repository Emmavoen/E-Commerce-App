using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerceApp.Application.Contracts.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByColumnAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllByColumn(Expression<Func<T, bool>> predicate);
        Task DeleteAsync(int id);
        void UpdateASync(T entity);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByIdAsyncWithInclude(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsyncWithInclude(params Expression<Func<T, object>>[] includes);
    }
}