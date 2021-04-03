using SmartSaver.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace SmartSaver.Domain.Models
{
    public partial class User : IdentityModelBase
    {
        public User()
        {
            Goals = new HashSet<SavingGoal>();
            Transactions = new HashSet<Transaction>();
        }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        public double Cash { get; set; }

        public double Card { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string HashedPassword { get; set; }

        [Required]
        [Column(TypeName = "char(10)")]
        public string Role { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string FullName { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Base64UserImage { get; set; }

        public bool Logged { get; set; }

        public string Password { get; set; }

        public virtual ICollection<SavingGoal> Goals { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public double GetBalance(BalanceType balanceType)
        {
            return balanceType switch
            {
                BalanceType.Cash => Cash,
                BalanceType.Card => Card,
                _ => throw new Exception("Unknown balance type"),
            };
        }

        public void SetBalance(BalanceType balanceType, double amount)
        {
            switch (balanceType)
            {
                case BalanceType.Cash:
                    Cash = amount;
                    break;
                case BalanceType.Card:
                    Card = amount;
                    break;
                default:
                    throw new Exception("Unknown balance type");
            }
        }
    }

    public static class UserRole
    {
        public const string Admin = "admin";
        public const string User = "user";
    }

    public static class UserUtilities
    {
        public static string EncryptPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var saltedPassword = string.Format("{0}{1}", salt, password);
            //return sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
            return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
        }
    }
}
