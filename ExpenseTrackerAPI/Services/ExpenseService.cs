using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Services
{
    public class ExpenseService : Service<Expense>, IExpenseService
    {
        private readonly AppDbContext _appDbContext;

        public ExpenseService(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Expense?> createExpenseAsync(Expense expense)
        {
            var existingExpense = await CheckExpenseDuplicate(expense);
            var wallet = await _appDbContext.wallet.FirstOrDefaultAsync();

            if (wallet == null)
                return null;

            if (existingExpense != null)
                return null;

            _appDbContext.Expenses.Add(expense);

             wallet.Balance += expense.TransactionType == TransactionType.Income
                ? expense.Amount
                : -expense.Amount;
            wallet.LastUpdated = DateTime.UtcNow;

            await _appDbContext.SaveChangesAsync();
            return expense;
        }

        public async Task<Expense?> UpdateAsync(int id, Expense entity)
        {
            var transaction = await _appDbContext.Database.BeginTransactionAsync();

            var existingExpense = await _appDbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
            if (existingExpense == null)
                return null;

            var wallet = await _appDbContext.wallet.FirstOrDefaultAsync();
            if (wallet == null)
                return null;
            
            wallet.Balance += existingExpense.TransactionType == TransactionType.Income ? -existingExpense.Amount : existingExpense.Amount;
            wallet.Balance += entity.TransactionType == TransactionType.Income ? entity.Amount : -entity.Amount;
            wallet.LastUpdated = DateTime.UtcNow;

            existingExpense.Title = entity.Title;
            existingExpense.Amount = entity.Amount;
            existingExpense.DateOfEmission = entity.DateOfEmission?.ToUniversalTime();
            existingExpense.ExpenseTypeId = entity.ExpenseTypeId;
            existingExpense.Tin = entity.Tin;

            await _appDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return existingExpense;

        }

        public async Task<Expense?> DeleteExpenseAsync(int id)
        {
            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            var expense = await _appDbContext.Expenses.FindAsync(id);
            if (expense == null)
                return null;

            var wallet = await _appDbContext.wallet.FirstOrDefaultAsync();
            if (wallet == null)
                return null;

            wallet.Balance += expense.TransactionType == TransactionType.Income
                ? -expense.Amount
                : expense.Amount;
            wallet.LastUpdated = DateTime.UtcNow;

            await _appDbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return expense;
        }

        public async Task<Expense?> CheckExpenseDuplicate(Expense expense)
        {
            return await _appDbContext.Set<Expense>().FirstOrDefaultAsync(e => 
                e.Title == expense.Title &&
                e.Amount == expense.Amount &&
                e.DateOfEmission.HasValue && 
                expense.DateOfEmission.HasValue &&
                e.DateOfEmission.Value.Date == expense.DateOfEmission.Value.Date);
        }

    }
}
