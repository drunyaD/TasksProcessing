using TasksProcessing.DataAccess.EF.Entities;
using TasksProcessing.DataAccess.Interfaces;
using TasksProcessing.Logic.DTO;
using TasksProcessing.Logic.Interfaces;
using TasksProcessing.Logic.Requests;

namespace TasksProcessing.Logic.Services;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;

    public TaskService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TaskDto> CreateAsync(CreateTaskRequest createTaskRequest)
    {
        var newTask = new TaskEntity
        {
            Description = createTaskRequest.Description,
            TransferCount = 0
        };

        await _unitOfWork.Tasks.CreateAsync(newTask);

        await _unitOfWork.SaveChangesAsync();

        var userToAssign = await _unitOfWork.Users.GetRandomUserAsync();
        if (userToAssign is null)
        {
            newTask.State = TaskState.Waiting;
        }
        else
        {
            newTask.CurrentUser = userToAssign;
            newTask.State = TaskState.InProgress;
        }

        var allUsers = _unitOfWork.Users.GetAll();
        newTask.UsersToAssign = allUsers.ToList();

        await _unitOfWork.SaveChangesAsync();

        return MapToDto(newTask);
    }

    public async Task<TaskDto?> GetAsync(int taskId)
    {
        var task = await _unitOfWork.Tasks.GetAsync(taskId);
        if (task is null)
        {
            return null;
        }
        else
        {
            return MapToDto(task);
        }
    }

    public IEnumerable<TaskDto> GetAll()
    {
        var tasks = _unitOfWork.Tasks.GetAll();
        return tasks.Select(MapToDto);
    }

    public async Task ReassignTasksAsync()
    {
        var allTasks = _unitOfWork.Tasks.GetNotCompletedWithUsersToAssign();
        foreach(var task in allTasks)
        {
            var usersToAssign = task.UsersToAssign!;
            var taskIsCompleted = task.TransferCount >= 3 && usersToAssign.Count == 0;
            if (taskIsCompleted)
            {
                task.CurrentUserId = null;
                task.State = TaskState.Completed;
            }
            else
            {
                var userToAssign = await _unitOfWork.Users.GetRandomUserAsync(task.CurrentUserId);
                if (userToAssign is null)
                {
                    task.CurrentUserId = null;
                    task.State = TaskState.Waiting;
                }
                else
                {
                    task.TransferCount++;
                    task.CurrentUserId = userToAssign.Id;
                    var userFromCollection = usersToAssign.FirstOrDefault(u => u.Id == userToAssign.Id);
                    if (userFromCollection is not null)
                    {
                        usersToAssign.Remove(userFromCollection);
                    }       
                }
            }
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private TaskDto MapToDto(TaskEntity taskEntity)
    {
        return new TaskDto
        {
            Id = taskEntity.Id,
            Description = taskEntity.Description,
            CurrentUser = taskEntity.CurrentUser?.Name,
            State = taskEntity.State,
            TransferCount = taskEntity.TransferCount
        };
    }
}
