using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services.Interfaces
{
    public interface IExpenseService : IService<Expense>
    {
        Task<Expense> CheckExpenseDuplicate(Expense entity);
        Task<Expense> UpdateAsync(int id, Expense entity);
    }
}
