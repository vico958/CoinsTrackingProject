using CoinsTracking.Models;
using Microsoft.EntityFrameworkCore;
namespace CoinsTracking.Service.Repo
{
    public class DatabaseSettings:DbContext
    {
        private readonly string connetionString = "Server=coin.cluster-cftf4wan07m7.us-east-2.rds.amazonaws.com;Database=Coins;Uid=admin;Pwd=Vi159753ko;";
        public virtual DbSet<CoinsTable> coins { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(connetionString).EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoinsTable>()
                .HasKey(c => c.CoinName);

            modelBuilder.Entity<CoinsTable>()
                .Property(c => c.CreateDate);

            modelBuilder.Entity<CoinsTable>()
                .Property(c => c.LastUpdated);

            modelBuilder.Entity<CoinsTable>()
    .Property(c => c.Id);

            modelBuilder.Entity<CoinsTable>()
.Property(c => c.PriceUsd);

            base.OnModelCreating(modelBuilder);
        }
    }
}
