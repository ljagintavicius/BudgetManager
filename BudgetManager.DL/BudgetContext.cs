using BudgetManager.DL.Models;
using System.Data.Entity;

namespace BudgetManager.DL
{
    public class BudgetContext : DbContext
    {
        public BudgetContext() : base("BudgetDB")
        {
            Database.SetInitializer(new BudgetContextInitializer());

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }

    }
}
