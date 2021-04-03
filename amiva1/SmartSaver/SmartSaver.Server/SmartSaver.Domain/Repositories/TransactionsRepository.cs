using Microsoft.EntityFrameworkCore;
using SmartSaver.Domain.Models;
using SmartSaver.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSaver.Domain.Repositories
{
    public class TransactionsRepository : GenericRepository<Transaction>, ITransactionsRepository
    {
        public TransactionsRepository(SmartSaverContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Transaction>> GetSortedUserTransactions(Guid userId, SortingModel sortingModel)
        {
            return await sortingModel
                .OrderByColumn(Set.Where(i => i.UserId == userId))
                .Include(u => u.Category)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Transaction>> GetLastTransactions(Guid userId, int count = 5)
        {
            return await Set
                .Where(i => i.UserId == userId)
                .Include(i => i.Category)
                .OrderByDescending(i => i.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<GroupedTransaction>> GetAmountSpentPerCategory(Guid userId)
        {
            return await Set
                .Where(i => i.UserId == userId)
                .Include(i => i.Category)
                .GroupBy(i => i.Category.Name)
                .Select(g => new GroupedTransaction()
                {
                    Key = g.Key,
                    Sum = g.Sum(s => s.Amount)
                })
                .OrderByDescending(i => i.Sum)
                .ToListAsync();
        }
    }
}