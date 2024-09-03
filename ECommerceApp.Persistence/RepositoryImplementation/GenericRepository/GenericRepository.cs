using System.Linq.Expressions;
using ECommerceApp.Application.Contracts.GenericRepository;
using ECommerceApp.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<T>> GetAllAsyncWithInclude(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        
    }

}
