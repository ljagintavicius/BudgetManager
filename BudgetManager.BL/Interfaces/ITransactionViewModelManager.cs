using System.Collections.Generic;

namespace BudgetManager.BL.Services
{
    public interface ITransactionViewModelManager
    {
        List<TransactionViewModel> GetAllOrUpdate();
    }
}