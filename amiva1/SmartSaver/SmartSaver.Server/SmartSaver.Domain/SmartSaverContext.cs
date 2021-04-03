using Microsoft.EntityFrameworkCore;
using SmartSaver.Domain.Models;
using System;

#nullable disable

namespace SmartSaver.Domain
{
    public class SmartSaverContext : DbContext
    {
        public SmartSaverContext(DbContextOptions<SmartSaverContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SavingGoal> Goals { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SavingGoal>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.GoalName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
                /*
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Goals_Users");
                */
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.BalanceType)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_Categories");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Transactions_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                //entity.Property(e => e.Base64UserImage).HasColumnType("image");
            });

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = new Guid("6e33fa08-bc0f-438c-a21b-bcf4fc227661"),
                    FullName = "Test Testington",
                    //Username = "test_user",
                    HashedPassword = "hashed_password",
                    Email = "test@gmail.com",
                    Role = UserRole.Admin,
                });
        }
    }
}
