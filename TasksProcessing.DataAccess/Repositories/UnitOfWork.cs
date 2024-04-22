using TasksProcessing.DataAccess.EF;
using TasksProcessing.DataAccess.Interfaces;

namespace TasksProcessing.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TaskProcessingContext _taskProcessingContext;

    public ITaskRepository Tasks { get; }

    public IUserRepository Users { get; }

    public UnitOfWork(
        TaskProcessingContext taskProcessingContext,
        ITaskRepository tasks,
        IUserRepository users)
    {
        _taskProcessingContext = taskProcessingContext;
        Tasks = tasks;
        Users = users;
    }

    public async Task SaveChangesAsync()
    {
        await _taskProcessingContext.SaveChangesAsync();
    }
}
