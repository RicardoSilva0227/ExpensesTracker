using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Services
{
    public class CurrencyService : Service<Currency>, ICurrencyService
    {
        private readonly AppDbContext _appDbContext;

        public CurrencyService(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Currency> UpdateAsync(int id, Currency entity)
        {
            var existingCurrency = await _appDbContext.Currency.FirstOrDefaultAsync(e => e.Id == id);
            if (existingCurrency == null)
                return null;

            existingCurrency.Name = entity.Name;
            existingCurrency.Acronym = entity.Acronym;
            existingCurrency.Symbol = entity.Symbol;
            existingCurrency.DecimalPlaces = entity.DecimalPlaces;
            existingCurrency.CultureCode = entity.CultureCode;
            existingCurrency.ExchangeRateToBase = entity.ExchangeRateToBase;
            existingCurrency.DateOfCreation = entity.DateOfCreation;
            existingCurrency.LastUpdated = entity.LastUpdated;
            existingCurrency.IsDefault = entity.IsDefault;
            existingCurrency.IsCrypto = entity.IsCrypto;
            existingCurrency.Country = entity.Country;

            await _appDbContext.SaveChangesAsync();
            return existingCurrency;
        }
    }
}
