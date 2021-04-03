using SmartSaver.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSaver.Domain.Repositories.Interfaces
{
    public interface ITransactionsRepository : IGenericRepository<Transaction>
    {
        Task<IReadOnlyList<Transaction>> GetSortedUserTransactions(Guid userId, SortingModel sortingModel);

        Task<IReadOnlyList<Transaction>> GetLastTransactions(Guid userId, int count = 5);

        Task<IReadOnlyList<GroupedTransaction>> GetAmountSpentPerCategory(Guid userId);
    }
}