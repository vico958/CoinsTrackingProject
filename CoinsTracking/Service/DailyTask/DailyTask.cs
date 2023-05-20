using System.Timers;

namespace CoinsTracking.Service
{
    public static class DailyTask
    {
        static System.Timers.Timer timer;
        public static void RunEveryDay()
        {
            timer = new System.Timers.Timer(2 * 60 * 100);
            timer.Elapsed += TimerElapsedAsync;
            timer.Start();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            timer.Stop();
            timer.Dispose();
        }
        private static async void TimerElapsedAsync(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Executing the task...");
            Server server = new Server();
            await server.UpdateCoinsData();
            await server.PrintAllCoinsData();
            Console.WriteLine("Task completed.");
        }
    }
}
