using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services.Interfaces;

namespace ExpenseTrackerAPI.Services
{
    public class ExpenseService : Service<Expense>, IExpenseService
    {
        private readonly AppDbContext _appDbContext;

        public ExpenseService(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<Expense> UpdateAsync(Expense entity)
        {
            throw new NotImplementedException();
        }
    }
}
