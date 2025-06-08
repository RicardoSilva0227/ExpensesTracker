using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services.Interfaces
{
    public interface IWalletService : IService<Wallet>
    {
        Task<Wallet> GetWalletSummary(int id);
    }
}
