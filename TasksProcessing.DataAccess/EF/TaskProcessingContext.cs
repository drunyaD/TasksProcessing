using Microsoft.EntityFrameworkCore;
using TasksProcessing.DataAccess.EF.Entities;

namespace TasksProcessing.DataAccess.EF;

public class TaskProcessingContext : DbContext
{
    public DbSet<TaskEntity> Tasks { get; set; }

    public DbSet<UserEntity> Users { get; set; }

    public TaskProcessingContext(DbContextOptions<TaskProcessingContext> contextOptions)
        : base(contextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskEntity>()
            .HasMany(t => t.UsersToAssign)
            .WithMany(u => u.TasksToWork);

        modelBuilder.Entity<TaskEntity>()
            .HasOne(t => t.CurrentUser)
            .WithMany(u => u.AssignedTasks);

        modelBuilder.Entity<TaskEntity>()
            .Property(t => t.State)
            .HasConversion<int>();

        modelBuilder.Entity<UserEntity>()
           .HasIndex(u => u.Name)
           .IsUnique();
    }
}
