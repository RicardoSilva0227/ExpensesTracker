using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services.Interfaces;

namespace ExpenseTrackerAPI.Services
{
    public class ExpenseTypeService : Service<ExpenseType>, IExpenseTypeService
    {
        private readonly AppDbContext _appDbContext;

        public ExpenseTypeService(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<ExpenseType> UpdateAsync(ExpenseType entity)
        {
            throw new NotImplementedException();
        }
    }
}
