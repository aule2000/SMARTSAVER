using System;
using System.Collections.Generic;

namespace SmartSaver.Domain.Models
{
    public partial class Category : IdentityModelBase
    {
        public Category()
        {
            Transactions = new HashSet<Transaction>();
        }

        public string Name { get; set; }

        public Guid UserId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}