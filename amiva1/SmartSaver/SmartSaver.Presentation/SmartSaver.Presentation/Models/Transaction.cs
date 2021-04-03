using System;
using System.ComponentModel.DataAnnotations;

namespace SmartSaver.Presentation.Models
{
    public class Transaction
    {
        public int Amount { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CategoryId { get; set; }

        public string CategoryIdString {
            get => CategoryId.ToString();
            set => CategoryId = Guid.TryParse(value, out Guid g) ? g : default;
        }

        public virtual Category Category { get; set; }

        public virtual User User { get; set; }
        
        [Required]
        public string BalanceType { get; set; }

        [Required]
        public double AmountDouble
        {
            get => (double)Amount / 100;
            set => Amount = (int)(value * 100);
        }
    }
}