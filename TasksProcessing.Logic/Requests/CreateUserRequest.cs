using System.ComponentModel.DataAnnotations;

namespace TasksProcessing.Logic.Requests;

public record CreateUserRequest([Required] string Name);