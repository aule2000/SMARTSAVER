using Microsoft.EntityFrameworkCore;
using SmartSaver.Domain.Models;
using SmartSaver.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSaver.Domain.Repositories
{
    public class SavingsRepository : GenericRepository<SavingGoal>, ISavingGoalsRepository
    {
        public SavingsRepository(SmartSaverContext context) : base(context)
        {
        }

        public async Task<SavingGoal> GetUserGoalIfExists(SavingGoal goal)
        {
            return await Set.SingleOrDefaultAsync(i => i.GoalName == goal.GoalName);
        }

        public async Task<IReadOnlyList<SavingGoal>> GetUserGoals(Guid userId)
        {
            return await Set.Where(i => i.UserId == userId).ToListAsync();
        }

        public async Task<IReadOnlyList<SavingGoal>> GetSortedGoals(Guid userId, SortingModel sortingModel)
        {
            return await sortingModel
                .OrderByColumn(Set.Where(i => i.UserId == userId))
                .ToListAsync();
        }
    }
}
