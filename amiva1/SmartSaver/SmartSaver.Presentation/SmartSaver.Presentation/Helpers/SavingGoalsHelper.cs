using SmartSaver.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSaver.Presentation.Helpers
{
    public class SavingGoalsHelper
    {
        public static SavingGoal[] GenerateMockData()
        {
            Guid guid = new Guid("6e33fa08-bc0f-438c-a21b-bcf4fc227661");
            SavingGoal[] goals = new SavingGoal[3];
            goals[0] = new SavingGoal
            {
                GoalName = "test1",
                Description = "testing",
                GoalAmount = 100.48,
                StartDate = DateTime.Now,
                FinishDate = DateTime.Today.AddDays(1),
                Progress = 0.48,
                UserId = guid
            };
            goals[1] = new SavingGoal
            {
                GoalName = "test2",
                Description = "testing",
                GoalAmount = 500,
                StartDate = DateTime.Now,
                FinishDate = DateTime.Today.AddDays(3),
                Progress = 120,
                UserId = guid
            };
            goals[2] = new SavingGoal
            {
                GoalName = "test3",
                Description = "testing",
                GoalAmount = 420,
                StartDate = DateTime.Now,
                FinishDate = DateTime.Today.AddDays(7),
                Progress = 300,
                UserId = guid
            };
            return goals;
        }

        public static List<SavingGoal> staticGoals = new List<SavingGoal>();

        public static SavingGoal[] _savingGoals;

        public static bool isAlreadyInit = false;
    }
}
