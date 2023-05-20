using System.Text.Json;
using CoinsTracking.Service.JsonObjects;
using CoinsTracking.Service.Coins;

namespace CoinsTracking.Service
{
    public class Server
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl = "https://api.coincap.io/v2/assets";
        private readonly CoinsManager coinsManager = new CoinsManager();
        public async Task UpdateCoinsData()
        {
            try
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiUrl}");
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                var json = await response.Content.ReadAsStringAsync();
                CoinList coinObject = JsonSerializer.Deserialize<CoinList>(json);
                List<CoinsTable> coinsTables = new List<CoinsTable>();
                foreach(var coin in coinObject.data)
                {
                    coinsTables.Add(new CoinsTable()
                    {
                        CoinName = coin.name,
                        PriceUsd = Decimal.Parse(coin.priceUsd),
                    }
                    );
                }
                await coinsManager.UpdateDatabaseAsync(coinsTables);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task PrintAllCoinsData()
        {
            try
            {
                var coins = await coinsManager.GetAllCoinsDataAsync();
                foreach(var coin in coins)
                {
                    Console.WriteLine($"Coin Name: {coin.CoinName}, Coin Usd Value: {coin.PriceUsd}, Coin Last Updated at: {coin.LastUpdated}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async Task Main(string[] args)
        {
            DailyTask.RunEveryDay();
        }
    }

}
