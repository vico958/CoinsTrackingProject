using CoinsTracking.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CoinsTracking.Service.Repo
{
    public class DatabaseRequests
    {
        private readonly DatabaseSettings db = new DatabaseSettings();
        public async Task<List<CoinsTable>> GetAllDataAsync()
        {
            return await db.coins.ToListAsync();
        }
        public async Task<bool> UpdateDatabaseAsync(List<CoinsTable> coinsToUpdate)
        {
            var listOfCoinsNames = coinsToUpdate.Select(coin => coin.CoinName);
            var alreadyExistingCoinsInDB = db.coins.Where(coin => listOfCoinsNames.Contains(coin.CoinName)).ToList();
            foreach (var coin in alreadyExistingCoinsInDB)
            {
                var coinUpdatedInfo = coinsToUpdate.FirstOrDefault(c => c.CoinName == coin.CoinName);
                if (coinUpdatedInfo != null)
                {
                    coin.PriceUsd = coinUpdatedInfo.PriceUsd;
                }
            }
            try
            {
                int updates = await db.SaveChangesAsync();
                Console.WriteLine(updates);
            }
            catch (Exception ex)
            {
                // Log or print the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
            var newCoinsToAddToDB = coinsToUpdate.Where(updateCoin => !alreadyExistingCoinsInDB.Any(coin => coin.CoinName == updateCoin.CoinName));
            if (newCoinsToAddToDB != null && newCoinsToAddToDB.Count() > 0)
            {

                try
                {
                    db.coins.AddRange(newCoinsToAddToDB);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Log or print the exception details
                    Console.WriteLine($"Exception: {ex.Message}");
                    Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                }
            }
            return true;
        }
        /*        public async Task<bool> UpdateDatabaseAsync(List<CoinsTable> coinsToUpdate)
                {
                        var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    CoinsTable coinsTable = db.coins.FirstOrDefault(x => x.CoinName.Equals("check2"));
                    Console.WriteLine($" i am  {coinsTable.CoinName}, {coinsTable.PriceUsd}");
                    coinsTable.PriceUsd = 1033;;
                        int updates = db.SaveChanges();
                    Console.WriteLine(updates);
                        transaction.Commit(); // solves the problem
                    return true;
                }*/
    }
}
