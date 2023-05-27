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
            _services.AddTransient<CoinsManager>();
            _services.AddTransient<DatabaseRequests>();
            _services.AddTransient<HttpRequestAndConsoleApp>();
            _services.AddTransient<DailyTask>();
            _services.AddTransient<DatabaseSettings>();
        }
    }
}
