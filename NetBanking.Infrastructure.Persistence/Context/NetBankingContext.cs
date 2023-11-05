using Microsoft.EntityFrameworkCore;
using NetBanking.Core.Domain.Entities;

namespace NetBanking.Infrastructure.Persistence.Context
{
    public class NetBankingContext : DbContext
    {
        public NetBankingContext(DbContextOptions<NetBankingContext> options) : base(options) { }

        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<CreditCard> Credits { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<SavingAccount> SavingAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> transactionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tables

            modelBuilder.Entity<Beneficiary>().ToTable("Beneficiaries");
            modelBuilder.Entity<CreditCard>().ToTable("CreditCards");
            modelBuilder.Entity<Loan>().ToTable("Loans");
            modelBuilder.Entity<SavingAccount>().ToTable("Saving Accounts");
            modelBuilder.Entity<Transaction>().ToTable("Transactions");
            modelBuilder.Entity<TransactionType>().ToTable("TransactionTypes");

            #endregion

            #region Keys

            modelBuilder.Entity<Beneficiary>().HasKey(x => x.Id);
            modelBuilder.Entity<CreditCard>().HasKey(x => x.Id);
            modelBuilder.Entity<Loan>().HasKey(x => x.Id);
            modelBuilder.Entity<SavingAccount>().HasKey(x => x.Id);
            modelBuilder.Entity<Transaction>().HasKey(x => x.Id);
            modelBuilder.Entity<TransactionType>().HasKey(x => x.Id);

            #endregion

            #region Relationships

            #region TransactionType
            
            modelBuilder.Entity<TransactionType>()
                .HasMany(x => x.Transactions)
                .WithOne(t => t.TransactionType)
                .HasForeignKey(x => x.TransactionTypeId)
                .HasConstraintName("fk_TransactionType_Transaction");
            #endregion

            #endregion

            #region Properties Configuration
            #endregion

        }
    }
}
