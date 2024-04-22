
using Autofac.Extensions.DependencyInjection;
using TasksProcessing.API;

namespace TasksProcessing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webHostBuilder => {
                    webHostBuilder.UseStartup<Startup>();
                })
                .Build();

            host.Run();
        }
    }
}
