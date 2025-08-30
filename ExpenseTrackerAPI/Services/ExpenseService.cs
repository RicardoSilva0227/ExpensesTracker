using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ExpenseTrackerAPI.Services
{
    public class ExpenseService : Service<Expense>, IExpenseService
    {
        private readonly AppDbContext _appDbContext;

        public ExpenseService(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // need to alter this later for a less mistake prone version
        public async Task<Expense> CheckExpenseDuplicate(Expense expense)
        {
            return await _appDbContext.Set<Expense>().FirstOrDefaultAsync(e => e.Title == expense.Title &&
                            e.Amount == expense.Amount &&
                            e.DateOfEmission.Value.Date == expense.DateOfEmission.Value.Date);
        }

        public async Task<Expense> UpdateAsync(int id, Expense entity)
        {
            var existingExpense = await _appDbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
            if (existingExpense == null)
                return null;

            existingExpense.Title = entity.Title;
            existingExpense.Amount = entity.Amount;
            existingExpense.DateOfEmission = entity.DateOfEmission;
            existingExpense.ExpenseTypeId = entity.ExpenseTypeId;
            existingExpense.Tin = entity.Tin;

            await _appDbContext.SaveChangesAsync();
            return existingExpense;

        }
    }
}
