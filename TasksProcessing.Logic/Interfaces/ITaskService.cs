using TasksProcessing.Logic.DTO;
using TasksProcessing.Logic.Requests;

namespace TasksProcessing.Logic.Interfaces;

public interface ITaskService
{
    Task<TaskDto> CreateAsync(CreateTaskRequest createTaskRequest);

    Task<TaskDto?> GetAsync(int taskId);

    IEnumerable<TaskDto> GetAll();

    Task ReassignTasksAsync();
}
