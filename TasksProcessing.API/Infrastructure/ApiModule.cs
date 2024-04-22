using Autofac;
using Microsoft.EntityFrameworkCore;
using TasksProcessing.DataAccess.EF;
using TasksProcessing.Logic.Interfaces;
using TasksProcessing.Logic.Services;

namespace TasksProcessing.API.Infrastructure;

public class ApiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TaskService>().As<ITaskService>().InstancePerLifetimeScope();
        builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
    }
}
