using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ExpenseTrackerAPI.Services
{
    public class ConfigService : Service<Configs>, IConfigService
    {
        private readonly AppDbContext _appDbContext;

        public ConfigService(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Configs> GetFirstOrDefault()
        {
            return await _appDbContext.Configs.FirstOrDefaultAsync();
        }

        public async Task<Configs> UpdateAsync(int id, Configs entity)
        {
            var existingConfigs = await _appDbContext.Configs.FirstOrDefaultAsync(e => e.Id == id);
            if (existingConfigs == null)
                return null;

            existingConfigs.UseFtp = entity.UseFtp;
            existingConfigs.FtpServer = entity.FtpServer;
            existingConfigs.FtpUsername = entity.FtpUsername;
            existingConfigs.FtpPassword = entity.FtpPassword;
            existingConfigs.FtpPort = entity.FtpPort;
            existingConfigs.SmtpServer = entity.SmtpServer;
            existingConfigs.SmtpPort = entity.SmtpPort;
            existingConfigs.SmtpUsername = entity.SmtpUsername;
            existingConfigs.SmtpPassword = entity.SmtpPassword;
            existingConfigs.Timezone = entity.Timezone;
            existingConfigs.DateFormat = entity.DateFormat;
            existingConfigs.EnableMultiCurrency = entity.EnableMultiCurrency;
            existingConfigs.EnableDiscounts = entity.EnableDiscounts;
            existingConfigs.LastUpdated = DateTime.Today;

            await _appDbContext.SaveChangesAsync();
            return existingConfigs;
        }
    }
}
