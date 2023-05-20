using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace CoinsTracking.Service
{
    public class CoinsDatabaseManager
    {
        public async Task<List<CoinsTable>> GetAllCoinsDataAsync(string connectionString)
        {
            List < CoinsTable > coinTableListToReturn = new List < CoinsTable >();
            try
            {
            using (SqlConnection connectionToCoinDatabase = SqlConnectionFunction(connectionString))
            {
                await connectionToCoinDatabase.OpenAsync();
                string sqlQuery = "SELECT * FROM Coins";
                SqlCommand command = new SqlCommand(sqlQuery, connectionToCoinDatabase);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    CoinsTable coinsTable = new CoinsTable();
                    coinsTable.Id = reader.GetInt32(0);
                    coinsTable.CoinName = reader.GetString(1);
                    coinsTable.PriceUsd = reader.GetDecimal(2);
                    coinsTable.LastUpdated = reader.GetDateTime(4);
                    coinTableListToReturn.Add(coinsTable);
                }
                await reader.CloseAsync();
                await connectionToCoinDatabase.CloseAsync();
            }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
                return coinTableListToReturn;
        }
        public async Task<bool> UpdateDatabaseAsync(string connectionString, List<CoinsTable> coinsToUpdate)
        {
            await UpdateCoinsTable(connectionString, coinsToUpdate);
                return true;
        }
        private static SqlConnection SqlConnectionFunction(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
        private static async Task UpdateCoinsTable(string connectionString, List<CoinsTable> coinsToUpdate)
        {
            DataTable coinDataTempTable = CreateAndFillTempCoinData(coinsToUpdate);
            using (SqlConnection connectionToCoinDatabase = SqlConnectionFunction(connectionString))
            {
                await connectionToCoinDatabase.OpenAsync();
                using (SqlCommand command = connectionToCoinDatabase.CreateCommand())
                {
                    string createTemporaryTable = "CREATE TABLE #TempCoins (CoinName VARCHAR(100), PriceUsd Money)";
                    string updateCoinsTable = @"
                        MERGE Coins AS target
                        USING #TempCoins AS source
                        ON target.CoinName = source.CoinName
                        WHEN MATCHED THEN
                            UPDATE SET target.PriceUsd = source.PriceUsd
                        WHEN NOT MATCHED THEN
                            INSERT (CoinName, PriceUsd)
                            VALUES (source.CoinName, ISNULL(source.PriceUsd, 0));
                    ";
                    string dropTempCoinsTable = "DROP TABLE #TempCoins";
                    await ExecuteCommandAsync(command, createTemporaryTable);
                    InsertCoinDataIntoTemporaryTable(coinDataTempTable, connectionToCoinDatabase);
                    await ExecuteCommandAsync(command, updateCoinsTable);
                    await ExecuteCommandAsync(command, dropTempCoinsTable);
                    await connectionToCoinDatabase.CloseAsync();
                }
            }

        }

        private static DataTable CreateAndFillTempCoinData(List<CoinsTable> coinsToUpdate)
        {
            DataTable coinDataTempTable = new DataTable();
            coinDataTempTable.Columns.Add("CoinName", typeof(string));
            coinDataTempTable.Columns.Add("PriceUsd", typeof(SqlMoney));
            coinDataTempTable.Columns.Add("CreateDate", typeof(DateTime));
            coinDataTempTable.Columns.Add("LastUpdated", typeof(DateTime));
            foreach (var coin in coinsToUpdate)
            {
                coinDataTempTable.Rows.Add(coin.CoinName, coin.PriceUsd);
            }

            return coinDataTempTable;
        }

        private static void InsertCoinDataIntoTemporaryTable(DataTable coinData, SqlConnection connectionToCoinDatabase)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionToCoinDatabase))
            {
                bulkCopy.DestinationTableName = "#TempCoins";
                bulkCopy.ColumnMappings.Add("CoinName", "CoinName");
                bulkCopy.ColumnMappings.Add("PriceUsd", "PriceUsd");
                bulkCopy.WriteToServer(coinData);
            }
        }

        private static async Task ExecuteCommandAsync(SqlCommand command, string commancdText)
        {
            command.CommandText = commancdText;
            await command.ExecuteNonQueryAsync();
        }
    }
}
