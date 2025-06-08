using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Services
{
    public class WalletService : Service<Wallet>, IWalletService
    {
        private readonly AppDbContext _appDbContext;

        public WalletService(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Wallet> GetWalletSummary(int id)
        {
            var currentMonthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var nextMonthStart = currentMonthStart.AddMonths(1);

            var wallet = await _appDbContext.wallet
                .Include(w => w.User)
                .FirstOrDefaultAsync(w => w.UserId == id);

            if (wallet == null)
                return null;

            var entries = await _appDbContext.walletEntries
                .Where(e => e.WalletId == wallet.Id && e.CreatedAt >= currentMonthStart && e.CreatedAt < nextMonthStart)
                .ToListAsync();

            wallet.TransactionsNumber = entries.Count;
            wallet.TotalSpent = entries
                .Where(e => e.Amount < 0) // only expenses
                .Sum(e => Math.Abs(e.Amount)); // convert to positive

            return wallet;
        }
    }
}
