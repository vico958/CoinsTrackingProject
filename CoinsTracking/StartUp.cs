using CoinsTracking.Service;
using CoinsTracking.Service.Coins;
using Microsoft.Extensions.DependencyInjection;

namespace CoinsTracking
{
    public class Startup
    {
        private readonly IServiceCollection _services;

        public Startup(IServiceCollection services)
        {
            _services = services;
        }

        public void ConfigureServices()
        {
            // Register your services here
            _services.AddSingleton<CoinsManager>();
            _services.AddSingleton<DatabaseRequests>();
            _services.AddSingleton<HttpRequestAndConsoleApp>();
            _services.AddSingleton<DailyTask>();
            _services.AddSingleton<DatabaseSettings>();
        }
    }
}
