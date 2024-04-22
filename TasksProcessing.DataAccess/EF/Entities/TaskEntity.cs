using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksProcessing.DataAccess.EF.Entities;

[Table("Tasks")]
public class TaskEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Description { get; set; } = null!;

    public TaskState State { get; set; }

    public int TransferCount { get; set; }

    public int? CurrentUserId { get; set; }

    public UserEntity? CurrentUser { get; set; }

    public ICollection<UserEntity>? UsersToAssign { get; set; }
}