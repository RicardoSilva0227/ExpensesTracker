using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Services
{
    public class ExpenseTypeService : Service<ExpenseType>, IExpenseTypeService
    {
        private readonly AppDbContext _appDbContext;

        public ExpenseTypeService(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ExpenseType> UpdateAsync(int id, ExpenseType entity)
        {
            var existingExpenseType = await _appDbContext.ExpenseTypes.FirstOrDefaultAsync(e => e.Id == id);
            if (existingExpenseType == null)
                return null;

            existingExpenseType.Code = entity.Code;
            existingExpenseType.Description = entity.Description;
            existingExpenseType.Icon = entity.Icon;

            await _appDbContext.SaveChangesAsync();
            return existingExpenseType;
        }
    }
}
