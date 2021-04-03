using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSaver.Presentation.Models
{
    public class SavingGoal
    {
        public Guid Id { get; set; }
        [Required]
        public string GoalName { get; set; }

        public string Description { get; set; }
        [Required]
        [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than zero.")]
        public double GoalAmount { get; set; }

        public DateTime StartDate { get; set; }
        [Required]
        public DateTime FinishDate { get; set; }

        public double Progress { get; set; }

        public Guid UserId { get; set; }

        public double ProgressPercentageValue
        {
            get => (Progress / GoalAmount) * 100;
        }
    }
}