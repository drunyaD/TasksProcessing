using System.ComponentModel.DataAnnotations;

namespace TasksProcessing.Logic.Requests;

public record CreateTaskRequest([Required] string Description);