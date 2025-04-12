using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services.Interfaces
{
    public interface IExpenseTypeService : IService<ExpenseType>
    {
        Task<ExpenseType> UpdateAsync(int id, ExpenseType entity);
    }
}
