using CoinsTracking.Models;

namespace CoinsTracking.Service.Coins
{
    public class CoinsManager
    {
        private readonly DatabaseRequests _databaseRequestsManager;
        public CoinsManager(DatabaseRequests databaseRequests) {
            _databaseRequestsManager = databaseRequests;
        }
        public async Task<List<CoinsTable>> GetAllCoinsDataAsync()
        {
            return await _databaseRequestsManager.GetAllDataAsync();
        }
        public async Task<bool> UpdateDatabaseAsync(List<CoinsTable> coinsToUpdate)
        {
            return await _databaseRequestsManager.UpdateDatabaseAsync(coinsToUpdate);
        }
    }
}
