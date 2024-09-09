using System.Linq.Expressions;
using ECommerceApp.Application.Contracts.GenericRepository;
using ECommerceApp.Application.Helper;
using ECommerceApp.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ECommerceApp.Persistence.RepositoryImplementation.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        protected DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await _dbSet.AddAsync(entity);
            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _dbSet.FindAsync(id);
            _dbSet.Remove(existing);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var result = await _dbSet.ToListAsync();
            // await appDbContext.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllByColumn(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public async Task<T> GetByColumnAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public void UpdateASync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

        }


        public async Task<T> GetByIdAsyncWithInclude(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<IEnumerable<T>> GetAllAsyncWithInclude(Expression<Func<T, bool>> filter = null,Expression<Func<T,
        object>> orderBy = null, bool ascending = true,params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if(filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }
            return await query.ToListAsync();
        }


        public async Task<PaginatedList<T>> GetPaginatedAsync(
        Expression<Func<T, bool>> predicate,
        int pageNumber,
        int pageSize,
        Expression<Func<T, object>> orderBy = null,
        bool ascending = true,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
        IQueryable<T> query = _dbSet;

        
        if (include != null)
        {
            query = include(query);
        }

        
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        
        if (orderBy != null)
        {
            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
        }

        var count = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }


    }

}
