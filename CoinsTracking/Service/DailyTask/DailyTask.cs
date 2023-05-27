using System.Timers;

namespace CoinsTracking.Service
{
    public class DailyTask
    {
        static System.Timers.Timer timer;
        private readonly HttpRequestAndConsoleApp _httpRequestAndConsoleApp;
        public DailyTask(HttpRequestAndConsoleApp httpRequestAndConsoleApp)
        {
            _httpRequestAndConsoleApp = httpRequestAndConsoleApp;
        }

        public void RunEveryDay()//assume it run each 24 hours and not each 2 min, better for the checking...
        {
            timer = new System.Timers.Timer(2 * 60 * 100);
            timer.Elapsed += TimerElapsedAsync;
            timer.Start();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            timer.Stop();
            timer.Dispose();
        }
        private async void TimerElapsedAsync(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Executing the task...");
            await _httpRequestAndConsoleApp.UpdateCoinsData();
            await _httpRequestAndConsoleApp.PrintAllCoinsData();
            Console.WriteLine("Task completed.");
        }
    }
}
