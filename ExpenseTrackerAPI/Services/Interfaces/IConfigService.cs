using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services.Interfaces
{
    public interface IConfigService : IService<Configs>
    {
        Task<Configs> UpdateAsync(int id, Configs entity);
        Task<Configs> GetFirstOrDefault();
    }
}
