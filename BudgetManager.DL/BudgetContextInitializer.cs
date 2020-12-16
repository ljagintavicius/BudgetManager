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
                new TransactionCategory {TransactionType = ETransactionType.Expense, TransactionCategoryName = "Entertainment"},
                new TransactionCategory {TransactionType = ETransactionType.Expense, TransactionCategoryName = "Food"},
                new TransactionCategory {TransactionType = ETransactionType.Expense, TransactionCategoryName = "Education"},
                new TransactionCategory {TransactionType = ETransactionType.Expense, TransactionCategoryName = "Taxes"},
                new TransactionCategory {TransactionType = ETransactionType.Expense, TransactionCategoryName = "Transport"},
                new TransactionCategory {TransactionType = ETransactionType.Expense, TransactionCategoryName = "Other"},
                new TransactionCategory {TransactionType = ETransactionType.Income, TransactionCategoryName = "Salary"},
                new TransactionCategory {TransactionType = ETransactionType.Income, TransactionCategoryName = "Gifts"},
                new TransactionCategory {TransactionType = ETransactionType.Income, TransactionCategoryName = "Other"}
            };
            context.TransactionCategories.AddRange(transactionCategories);
        }
    }
}