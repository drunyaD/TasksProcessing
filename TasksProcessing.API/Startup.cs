using Autofac;
using Quartz;
using TasksProcessing.API.Infrastructure;
using TasksProcessing.API.Jobs;
using TasksProcessing.Logic.Infrastructure;

namespace TasksProcessing.API;

public class Startup
{
    private const string EveryTwoMinutesCron = "0 0/2 * * * ?";
    private const string DbName = "TaskProcessingDB";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; private set; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddQuartz(q =>
        {
            var jobKey = new JobKey("TasksReassignJob");
            q.AddJob<TasksReassignJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("TasksReassignJob-trigger")
                .WithCronSchedule(EveryTwoMinutesCron));
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new LogicModule(DbName));
        builder.RegisterModule(new ApiModule());
    }

    public void Configure(
      IApplicationBuilder app,
      IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
