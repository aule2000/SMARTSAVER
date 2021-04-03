using Microsoft.EntityFrameworkCore;
using SmartSaver.Domain.Models;
using SmartSaver.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSaver.Domain.Repositories
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(SmartSaverContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Category>> GetAllUserCategories(Guid userId, int perPage = 10, int page = 1)
        {
            return await Set.Where(i => i.UserId == userId).Skip((page - 1) * perPage).Take(perPage).ToListAsync();
        }

        public async Task<int> GetCount(Guid userId)
        {
            return await Set.Where(i => i.UserId == userId).CountAsync();
        }

        public async Task<Category> GetCategoryByName(string category)
        {
            return await Set.SingleOrDefaultAsync(i => i.Name == category);
        }
    }
}