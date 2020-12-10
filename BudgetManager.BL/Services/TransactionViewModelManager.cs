using BudgetManager.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.BL.Services
{
    public class TransactionViewModelManager : ITransactionViewModelManager
    {
        private List<Transaction> _transactions;
        private ICRUDRepository<Transaction> _transactionManager;
        private List<TransactionViewModel> _transactionViewModels { get; set; }


        public TransactionViewModelManager()
        {
            _transactionViewModels = new List<TransactionViewModel>();
            _transactionManager = new TransactionManager();
            SetValues();
        }

        private void SetValues()
        {
            _transactions = _transactionManager.GetAll();
            foreach (var transaction in _transactions)
            {
                TransactionViewModel viewModel = new TransactionViewModel();
                viewModel.TransactionId = transaction.TransactionId;
                viewModel.TransactionType = Enum.GetName(typeof(ETransactionType), transaction.TransactionCategory.TransactionType);
                viewModel.DateTime = transaction.TransactionDate.ToString("yyyy-MM-dd");
                viewModel.TransactionCategory = transaction.TransactionCategory.TransactionCategoryName;
                viewModel.UserName = transaction.User.Name;
                viewModel.Amount = transaction.Sum;
                _transactionViewModels.Add(viewModel);
            }
        }

        public List<TransactionViewModel> GetAllOrUpdate()
        {
            _transactionViewModels = new List<TransactionViewModel>();
            SetValues();
            return _transactionViewModels;
        }
    }
}
