using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpenseTrackerAPI.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        public Service(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateAsync(T entity)
        {
            await _appDbContext.AddAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _appDbContext.Remove(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync(int pageSize = 10, int pageNumber = 1, params Expression<Func<T, object>>[] includes)
        {

            IQueryable<T> query = _appDbContext.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.OrderBy(e => EF.Property<DateTime>(e, "DateOfCreation"))
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _appDbContext.Set<T>().FirstOrDefaultAsync(filter);
        }

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

    }
}