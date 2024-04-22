
namespace TasksProcessing.DataAccess.Interfaces;

public interface IRepository<TEntity, TKey>
{
    Task<TEntity?> GetAsync(TKey key);

    IEnumerable<TEntity> GetAll(); 

    Task CreateAsync(TEntity entity);
}
