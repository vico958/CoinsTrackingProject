using CoinsTracking.Models.JsonObjects;
using CoinsTracking.Models;
using CoinsTracking.Service.Coins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoinsTracking.Service
{
    public class HttpRequestAndConsoleApp
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl = "https://api.coincap.io/v2/assets";
        private readonly CoinsManager _coinsManager;
        public HttpRequestAndConsoleApp(CoinsManager coinsManager) {
            _coinsManager = coinsManager;
        }
        public async Task UpdateCoinsData()
        {
            try
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiUrl}");
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                var json = await response.Content.ReadAsStringAsync();
                CoinList coinObject = JsonSerializer.Deserialize<CoinList>(json);
                List<CoinsTable> coinsTables = new List<CoinsTable>();
                foreach (var coin in coinObject.data)
                {
                    coinsTables.Add(new CoinsTable()
                    {
                        CoinName = coin.name,
                        PriceUsd = Decimal.Parse(coin.priceUsd),
                    }
                    );
                }
                await _coinsManager.UpdateDatabaseAsync(coinsTables);
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
                var coins = await _coinsManager.GetAllCoinsDataAsync();
                foreach (var coin in coins)
                {
                    Console.WriteLine($"Coin Name: {coin.CoinName}, Coin Usd Value: {coin.PriceUsd}, Coin Last Updated at: {coin.LastUpdated}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
