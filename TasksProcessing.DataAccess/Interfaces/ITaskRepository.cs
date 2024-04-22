using TasksProcessing.DataAccess.EF.Entities;

namespace TasksProcessing.DataAccess.Interfaces;

public interface ITaskRepository : IRepository<TaskEntity, int>
{
    IEnumerable<TaskEntity> GetNotCompletedWithUsersToAssign();
}
