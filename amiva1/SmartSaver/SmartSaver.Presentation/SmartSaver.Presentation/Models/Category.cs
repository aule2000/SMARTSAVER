using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SmartSaver.Presentation.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid UserId { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}