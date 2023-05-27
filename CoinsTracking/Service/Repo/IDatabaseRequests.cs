using CoinsTracking.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CoinsTracking.Service
{
    public interface IDatabaseRequests
    {
        Task<List<CoinsTable>> GetAllDataAsync();
        Task<bool> UpdateDatabaseAsync(List<CoinsTable> coinsToUpdate);
    }
}
