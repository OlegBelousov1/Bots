using CardsBot.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CardsBot.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private IQueryable<T> _query;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _query = _context.Set<T>();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public List<T> ToList()
        {
            return GetQuery().ToList();
        }

        public Task<List<T>> ToListAsync()
        {
            return GetQuery().ToListAsync();
        }

        public Task<List<T>> ToListAsync(Expression<Func<T, bool>> predicate)
        {
            return GetQuery().Where(predicate).ToListAsync();
        }

        public Task<List<T>> ToListAsync(CancellationToken token)
        {
            return GetQuery().ToListAsync(token);
        }

        public Task<List<T>> ToListAsync(Expression<Func<T, bool>> predicate, CancellationToken token)
        {
            return GetQuery().Where(predicate).ToListAsync(token);
        }

        public T FirstOrDefault()
        {
            return GetQuery().FirstOrDefault();
        }

        public Task<T> FirstOrDefaultAsync()
        {
            return GetQuery().FirstOrDefaultAsync();
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return GetQuery().Where(predicate).FirstOrDefaultAsync();
        }

        public IRepository<T> Include(params Expression<Func<T, object>>[] includes)
        {
            foreach (var include in includes)
            {
                _query = _query.Include(include);
            }
            return this;
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public ValueTask<EntityEntry<T>> AddAsync(T entity)
        {
            return _dbSet.AddAsync(entity);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return GetQuery().Any(predicate);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IRepository<T> OrderBy<K>(Expression<Func<T, K>> predicate)
        {
            _query = _query.OrderBy(predicate);
            return this;
        }

        public IRepository<T> GroupBy<K>(Expression<Func<T, K>> predicate)
        {
            _query.GroupBy(predicate);
            return this;
        }

        public IQueryable<T> GetQuery()
        {
            return _query;
        }

        public IRepository<T> OrderByDescending<K>(Expression<Func<T, K>> predicate)
        {
            _query = _query.OrderByDescending(predicate);
            return this;
        }
    }
}
