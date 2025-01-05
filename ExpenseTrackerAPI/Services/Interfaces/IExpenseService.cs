using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services.Interfaces
{
    public interface IExpenseService : IService<Expense>
    {
        Task<Expense> UpdateAsync(Expense entity);
    }
}
