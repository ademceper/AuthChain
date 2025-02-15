using System.Linq.Expressions;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
    Task<T> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task SoftDeleteAsync(Guid id);
    Task HardDeleteAsync(Guid id);
    Task SaveChangesAsync();
}
