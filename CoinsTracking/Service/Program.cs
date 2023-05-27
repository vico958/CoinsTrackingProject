using Microsoft.Extensions.DependencyInjection;

namespace CoinsTracking.Service
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            /*            DailyTask.RunEveryDay();*/
            var services = new ServiceCollection();

            // Configure services using the Startup class
            var startup = new Startup(services);
            startup.ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();
            try
            {
               serviceProvider.GetRequiredService<DailyTask>().RunEveryDay();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
