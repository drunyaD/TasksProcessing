using Autofac;
using Microsoft.EntityFrameworkCore;
using TasksProcessing.DataAccess.EF;
using TasksProcessing.DataAccess.Interfaces;
using TasksProcessing.DataAccess.Repositories;

namespace TasksProcessing.Logic.Infrastructure;

public class LogicModule : Module
{
    private readonly string _dbName;

    public LogicModule(string dbName)
    {
        _dbName = dbName;
    }

    protected override void Load(ContainerBuilder builder)
    {

        builder.Register(x =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskProcessingContext>();
            optionsBuilder.UseInMemoryDatabase(_dbName);
            return new TaskProcessingContext(optionsBuilder.Options);
        }).InstancePerLifetimeScope();

        builder.RegisterType<TaskRepository>().As<ITaskRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    }
}
