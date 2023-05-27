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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime LastUpdated { get; set; }
    }
}
