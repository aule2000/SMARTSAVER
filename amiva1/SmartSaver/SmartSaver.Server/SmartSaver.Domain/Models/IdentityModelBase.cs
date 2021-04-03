using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartSaver.Domain.Models
{
    public abstract class IdentityModelBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}