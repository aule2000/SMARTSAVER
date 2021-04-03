using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartSaver.Domain.Models
{
    public partial class SavingGoal : IdentityModelBase
    {
        public string GoalName { get; set; }

        public string Description { get; set; }

        public double GoalAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public double Progress { get; set; }

        public Guid UserId { get; set; }

        //public virtual User User { get; set; }

        [NotMapped]
        public double ProgressPercentageValue
        {
            get => (Progress / GoalAmount) * 100;
        }
    }
}
