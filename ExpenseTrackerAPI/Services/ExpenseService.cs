using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services.Interfaces;
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

        public Task<Expense> UpdateAsync(Expense entity)
        {
            throw new NotImplementedException();
        }
    }
}
