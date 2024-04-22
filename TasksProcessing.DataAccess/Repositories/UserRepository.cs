using Microsoft.EntityFrameworkCore;
using TasksProcessing.DataAccess.EF;
using TasksProcessing.DataAccess.EF.Entities;
using TasksProcessing.DataAccess.Interfaces;

namespace TasksProcessing.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbSet<UserEntity> _dbSet;

    public UserRepository(TaskProcessingContext tasksProcessingContext)
    {
        _dbSet = tasksProcessingContext.Users;
    }

    public async Task CreateAsync(UserEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<UserEntity?> GetRandomUserAsync(int? currentUserId = default)
    {
        return await _dbSet
            .OrderBy(u => Microsoft.EntityFrameworkCore.EF.Functions.Random())
            .FirstOrDefaultAsync(u => u.Id != currentUserId);
    }

    public async Task<UserEntity?> GetAsync(int key)
    {
        return await _dbSet.FindAsync(key);
    }

    public IEnumerable<UserEntity> GetAll()
    {
        return _dbSet.AsEnumerable();
    }

    public async Task<UserEntity?> FindByName(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Name == name);
    }
}
