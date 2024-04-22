using Microsoft.EntityFrameworkCore;
using TasksProcessing.DataAccess.EF;
using TasksProcessing.DataAccess.EF.Entities;
using TasksProcessing.DataAccess.Interfaces;

namespace TasksProcessing.DataAccess.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly DbSet<TaskEntity> _dbSet;

    public TaskRepository(TaskProcessingContext tasksProcessingContext)
    {;
        _dbSet = tasksProcessingContext.Tasks;
    }

    public async Task CreateAsync(TaskEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<TaskEntity?> GetAsync(int key)
    {
        return await _dbSet
            .Include(t => t.CurrentUser)
            .FirstOrDefaultAsync(x => x.Id == key);
    }

    public IEnumerable<TaskEntity> GetAll()
    {
        return _dbSet
            .Include(t => t.CurrentUser)
            .AsEnumerable();
    }

    public IEnumerable<TaskEntity> GetNotCompletedWithUsersToAssign()
    {
        return _dbSet
            .Where(t => t.State != TaskState.Completed)
            .Include(t => t.CurrentUser)
            .Include(t => t.UsersToAssign)
            .AsEnumerable();
    }
}
