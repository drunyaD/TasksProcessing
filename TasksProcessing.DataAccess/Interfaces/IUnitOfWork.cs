namespace TasksProcessing.DataAccess.Interfaces;

public interface IUnitOfWork
{
    ITaskRepository Tasks { get; }

    IUserRepository Users { get; }

    Task SaveChangesAsync();
}