using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinsTracking.Models
{
    [Table("Coins")]
    public class CoinsTable
    {
        public int Id { get; private set; }
        [Key]
        public string CoinName { get; set; }
        public decimal PriceUsd { get; set; }
        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdated { get; private set; }
    }
}
