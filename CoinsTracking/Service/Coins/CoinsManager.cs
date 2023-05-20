namespace CoinsTracking.Service.Coins
{
    public class CoinsManager
    {//first connectionString it was for working on local at the begging i left it to be here because if i want to change...
        private readonly string connectionString = "Server=.;Database=Coins;Trusted_Connection=True;";
        private readonly string connectionStringWithAzure = "Server=tcp:coins-server.database.windows.net,1433;Initial Catalog=Coins;Persist Security Info=False;User ID=viktorDabush;Password=Vd123456789!@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private readonly CoinsDatabaseManager databaseManager = new CoinsDatabaseManager();
        public async Task<List<CoinsTable>> GetAllCoinsDataAsync()
        {
            return await databaseManager.GetAllCoinsDataAsync(connectionStringWithAzure);
        }
        public async Task<bool> UpdateDatabaseAsync(List<CoinsTable> coinsToUpdate)
        {
            return await databaseManager.UpdateDatabaseAsync(connectionStringWithAzure, coinsToUpdate);
        }
    }
}
