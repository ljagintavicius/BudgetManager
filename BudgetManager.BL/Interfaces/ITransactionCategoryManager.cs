using BudgetManager.DL.Models;

namespace BudgetManager.BL.Services
{
    public interface ITransactionCategoryManager : ICRUDRepository<TransactionCategory>
    {
        TransactionCategory SelectTransactionCategoryrByName(string transactionCategoryName);
    }
}