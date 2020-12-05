using BudgetManager.DL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
