using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> wallet { get; set; }
        public DbSet<WalletEntries> walletEntries { get; set; }
        public DbSet<Configs> Configs { get; set; }
        public DbSet<Currency> Currency { get; set; }


        // seed static tables

        private static readonly DateTime SeedDate = new DateTime();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed currencies
            modelBuilder.Entity<Currency>().HasData(
                new Currency
                {
                    Id = 1,
                    Name = "Euro",
                    Acronym = "EUR",
                    Symbol = "€",
                    DecimalPlaces = 2,
                    CultureCode = "pt-PT",
                    IsDefault = true,
                    IsCrypto = false,
                    Country = "European Union",
                    DateOfCreation = SeedDate,
                   
                },
                new Currency
                {
                    Id = 2,
                    Name = "US Dollar",
                    Acronym = "USD",
                    Symbol = "$",
                    DecimalPlaces = 2,
                    CultureCode = "en-US",
                    IsDefault = false,
                    IsCrypto = false,
                    Country = "United States",
                    DateOfCreation = SeedDate,

                },
                new Currency
                {
                    Id = 3,
                    Name = "Japanese Yen",
                    Acronym = "JPY",
                    Symbol = "¥",
                    DecimalPlaces = 0,
                    CultureCode = "ja-JP",
                    IsDefault = false,
                    IsCrypto = false,
                    Country = "Japan",
                    DateOfCreation = SeedDate,

                }
            );
        }
    }
}
