using SmartSaver.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartSaver.Domain.Models
{
    public partial class Transaction : IdentityModelBase
    {
        public int Amount { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual User User { get; set; }

        public BalanceType BalanceType { get; set; }

        [NotMapped]
        public double AmountDouble
        {
            get => (double)Amount / 100;
            set => Amount = (int)(value * 100);
        }
    }
}