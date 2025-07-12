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
    }
}
