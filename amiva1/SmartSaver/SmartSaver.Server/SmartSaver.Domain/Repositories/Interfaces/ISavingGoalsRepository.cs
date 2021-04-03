using SmartSaver.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSaver.Domain.Repositories.Interfaces
{
    public interface ISavingGoalsRepository : IGenericRepository<SavingGoal>
    {
        Task<SavingGoal> GetUserGoalIfExists(SavingGoal goal);

        Task<IReadOnlyList<SavingGoal>> GetUserGoals(Guid userId);

        Task<IReadOnlyList<SavingGoal>> GetSortedGoals(Guid userId, SortingModel sortingModel);
    }
}