using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services.Interfaces
{
    public interface IExpenseTypeService : IService<ExpenseType>
    {
        Task<ExpenseType> UpdateAsync(ExpenseType entity);
    }
}
