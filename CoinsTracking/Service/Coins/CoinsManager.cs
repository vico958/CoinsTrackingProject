namespace CoinsTracking.Service.Coins
{
    public class CoinsManager
    {
        private readonly string connectionString = "Server=.;Database=Coins;Trusted_Connection=True;";
        private readonly string connectionStringWithAzure = "Server=tcp:coins-server.database.windows.net,1433;Initial Catalog=Coins;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;";
        private readonly string connectionStringWithAzure2 = "Server=tcp:coins-server.database.windows.net,1433;Initial Catalog=Coins;Persist Security Info=False;User ID=viktorDabush;Password=Vd123456789!@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private readonly CoinsDatabaseManager databaseManager = new CoinsDatabaseManager();
        public async Task<List<CoinsTable>> GetAllCoinsDataAsync()
        {
            return await databaseManager.GetAllCoinsDataAsync(connectionStringWithAzure2);
        }
        public async Task<bool> UpdateDatabaseAsync(List<CoinsTable> coinsToUpdate)
        {
            return await databaseManager.UpdateDatabaseAsync(connectionStringWithAzure2, coinsToUpdate);
        }
    }
}
