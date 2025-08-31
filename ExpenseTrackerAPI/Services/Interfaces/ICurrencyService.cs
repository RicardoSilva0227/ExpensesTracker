using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services.Interfaces
{
    public interface ICurrencyService : IService<Currency>
    {
        Task<Currency> UpdateAsync(int id, Currency entity);
    }
}
