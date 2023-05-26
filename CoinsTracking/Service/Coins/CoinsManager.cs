using CoinsTracking.Models;
using CoinsTracking.Service.Repo;

namespace CoinsTracking.Service.Coins
{
    public class CoinsManager
    {
        private readonly DatabaseRequests databaseRequestsManager = new DatabaseRequests();
        public async Task<List<CoinsTable>> GetAllCoinsDataAsync()
        {
            return await databaseRequestsManager.GetAllDataAsync();
        }
        public async Task<bool> UpdateDatabaseAsync(List<CoinsTable> coinsToUpdate)
        {
            return await databaseRequestsManager.UpdateDatabaseAsync(coinsToUpdate);
        }
    }
}
