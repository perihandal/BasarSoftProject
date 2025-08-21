using System.Linq.Expressions;

namespace App.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        ValueTask<T?> GetByIdAsync(int id);
        ValueTask<T> AddAsync(T enitiy);
        void Update(T enitiy);
        void Delete(T entity);
        Task<List<T>> GetAllAsync();



    }
}