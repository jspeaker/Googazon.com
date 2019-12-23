using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(GoogazonFunctions.Startup))]
namespace GoogazonFunctions
{
    public class Startup : FunctionsStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSignalRCore();
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            ConfigureServices(builder.Services);
        }
    }
}