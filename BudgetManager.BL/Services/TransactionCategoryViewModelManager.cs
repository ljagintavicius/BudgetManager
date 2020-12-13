using BudgetManager.BL.Models;
using BudgetManager.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.BL.Services
{
    public class TransactionCategoryViewModelManager
    {
        private readonly List<TransactionCategory> _transactionCategories;

        public TransactionCategoryViewModelManager(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
            ITransactionCategoryManager _transactionCategoryManager = new TransactionCategoryManager();
            _transactionCategories = _transactionCategoryManager.GetAll();
        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public List<TransactionCategoryViewModel> GetDateSortedTransactionCategoriesAndSum (ETransactionType eTransactionType)
        {
            List<TransactionCategoryViewModel> _transactionCategoryViewModels = new List<TransactionCategoryViewModel>();
            foreach (var transactionCategory in _transactionCategories.Where(z=>z.TransactionType==eTransactionType))
            {
                _transactionCategoryViewModels.Add(new TransactionCategoryViewModel()
                {
                    TransactionCategoryName = transactionCategory.TransactionCategoryName,
                    Amount = transactionCategory
                    .Transactions
                    .Where(z => z.TransactionDate >= StartTime && z.TransactionDate < EndTime)
                    .Sum(z => z.Sum)
                });
            }
            _transactionCategoryViewModels.Add(GetTotalInCategories(eTransactionType));
            return _transactionCategoryViewModels;
        }

        private TransactionCategoryViewModel GetTotalInCategories (ETransactionType eTransactionType)
        {
            TransactionCategoryViewModel totalInCategories = new TransactionCategoryViewModel()
            {
                TransactionCategoryName = "TOTAL",
                Amount = _transactionCategories
                            .Where(x => x.TransactionType == eTransactionType)
                            .Select(x => x.Transactions
                                            .Where(y => y.TransactionDate >= StartTime && y.TransactionDate < EndTime)
                                            .Sum(z=>z.Sum))
                            .Sum()
            };
            return totalInCategories;
        }



    }
}
