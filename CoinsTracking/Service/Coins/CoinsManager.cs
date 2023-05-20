namespace CoinsTracking.Service.Coins
{
    public class CoinsManager
    {
        private readonly string connectionString = "Server=.;Database=Coins;Trusted_Connection=True;";
        private readonly CoinsDatabaseManager databaseManager = new CoinsDatabaseManager();
        public async Task<List<CoinsTable>> GetAllCoinsDataAsync()
        {
            return await databaseManager.GetAllCoinsDataAsync(connectionString);
        }
        public async Task<bool> UpdateDatabaseAsync(List<CoinsTable> coinsToUpdate)
        {
            return await databaseManager.UpdateDatabaseAsync(connectionString, coinsToUpdate);
        }
    }
}
