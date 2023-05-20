namespace CoinsTracking
{
    public class CoinsTable
    {
        public int Id { get; set; }
        public string CoinName { get; set; }
        public decimal PriceUsd { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
