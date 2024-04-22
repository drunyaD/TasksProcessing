using Quartz;
using TasksProcessing.Logic.Interfaces;

namespace TasksProcessing.API.Jobs;

public class TasksReassignJob : IJob
{
    private readonly ITaskService _taskService;

    public TasksReassignJob(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Tasks reassigning start");

        await _taskService.ReassignTasksAsync();

        Console.WriteLine("Tasks reassigning finish");
    }
}
