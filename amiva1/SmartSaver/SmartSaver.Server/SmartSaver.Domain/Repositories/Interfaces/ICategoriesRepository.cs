using SmartSaver.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSaver.Domain.Repositories.Interfaces
{
    public interface ICategoriesRepository : IGenericRepository<Category>
    {
        Task<IReadOnlyList<Category>> GetAllUserCategories(Guid userId, int perPage = 10, int page = 1);

        Task<Category> GetCategoryByName(string category);

        Task<int> GetCount(Guid userId);
    }
}
