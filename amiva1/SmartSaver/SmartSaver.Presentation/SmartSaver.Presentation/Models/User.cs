using System;
using System.Collections.Generic;
using System.Text;
using SmartSaver.Presentation.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSaver.Presentation.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Gmail { get; set; }
        [Required]
        [Range(0, double.MaxValue,
            ErrorMessage = "Amount must be non-negative")]
        public double Cash { get; set; }
        [Required]
        [Range(0, double.MaxValue,
            ErrorMessage = "Amount must be non-negative")]
        public double Card { get; set; }

        public string FullName { get; set; }

        public byte[] UserImage { get; set; }

        public bool Logged { get; set; }

        public string Password { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
