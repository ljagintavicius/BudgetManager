using BudgetManager.DL.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace BudgetManager.DL
{
    class BudgetContextInitializer : CreateDatabaseIfNotExists<BudgetContext>
    {
        protected override void Seed(BudgetContext context)
        {
            List<TransactionCategory> transactionCategories = new List<TransactionCategory>
            {
                new TransactionCategory {TransactionType = ETransactionType.Expense, TransactionCategoryName = "Pramogos"},
                new TransactionCategory {TransactionType = ETransactionType.Expense, TransactionCategoryName = "Maistas"},
                new TransactionCategory {TransactionType = ETransactionType.Expense, TransactionCategoryName = "Mokslas"},
                new TransactionCategory {TransactionType = ETransactionType.Expense, TransactionCategoryName = "Mokesčiai"},
                new TransactionCategory {TransactionType = ETransactionType.Expense, TransactionCategoryName = "Transportas"},
                new TransactionCategory {TransactionType = ETransactionType.Expense, TransactionCategoryName = "Kita"},
                new TransactionCategory {TransactionType = ETransactionType.Income, TransactionCategoryName = "Atlyginimas"},
                new TransactionCategory {TransactionType = ETransactionType.Income, TransactionCategoryName = "Dovanos"},
                new TransactionCategory {TransactionType = ETransactionType.Income, TransactionCategoryName = "Laimėjimai"},
                new TransactionCategory {TransactionType = ETransactionType.Income, TransactionCategoryName = "Kita"}
            };
            context.TransactionCategories.AddRange(transactionCategories);
            List<User> userList = new List<User>
            {
                new User {Name = "PirmasVartotojas"},
                new User {Name = "AntrasVartotojas"},
                new User {Name = "TrečiasVartotojas"}
            };
            context.Users.AddRange(userList);
        }
    }
}