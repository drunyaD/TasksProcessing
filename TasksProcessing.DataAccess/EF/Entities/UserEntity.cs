using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksProcessing.DataAccess.EF.Entities;

[Table("Users")]
public class UserEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public ICollection<TaskEntity>? AssignedTasks { get; set; }

    public ICollection<TaskEntity>? TasksToWork { get; set; }
}