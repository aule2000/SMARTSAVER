using SmartSaver.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSaver.Presentation.Models
{
    public class SavingGoalsSortingModel
    {
        public SavingGoalsSortingEnum SortingColumn { get; set; } = SavingGoalsSortingEnum.StartDate;

        public bool IsAscending { get; set; } = true;
    }
}
