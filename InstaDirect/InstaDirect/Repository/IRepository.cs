using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace InstaDirect.Repository
{
    public interface IRepository<T> where T : class
    {
        void SaveChanges();
        Task SaveChangesAsync();
        List<T> ToList();
        Task<List<T>> ToListAsync(CancellationToken token);
        Task<List<T>> ToListAsync();
        Task<List<T>> ToListAsync(Expression<Func<T, bool>> predicate, CancellationToken token);
        Task<List<T>> ToListAsync(Expression<Func<T, bool>> predicate);
        T FirstOrDefault();
        Task<T> FirstOrDefaultAsync();
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        public IRepository<T> Include(params Expression<Func<T, object>>[] includes);
        public ValueTask<EntityEntry<T>> AddAsync(T entity);
        void Add(T entity);
        bool Any(Expression<Func<T, bool>> predicate);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        IRepository<T> OrderBy<K>(Expression<Func<T, K>> predicate);
        IRepository<T> OrderByDescending<K>(Expression<Func<T, K>> predicate);
        IRepository<T> GroupBy<K>(Expression<Func<T, K>> predicate);
        IQueryable<T> GetQuery();
    }
}
