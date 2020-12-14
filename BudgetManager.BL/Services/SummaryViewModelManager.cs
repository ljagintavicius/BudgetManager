using BudgetManager.BL.Models;
using BudgetManager.DL;
using BudgetManager.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.BL.Services
{
    public class SummaryViewModelManager
    {
        private readonly List<User> _users;
        private readonly List<TransactionCategory> _transactionCategories;
        public List<Transaction> Transactions { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Balance { get; set; }
        public List<UserTransactionsViewModel> UsersExpensesIncomes { get; set; }
        public List<TransactionCategoryViewModel> IncomesByCategory { get; set; }
        public List<TransactionCategoryViewModel> ExpensesByCategory { get; set; }

        public SummaryViewModelManager()
        {
            IUserManager _userManager = new UserManager();
            ITransactionCategoryManager _transactionCategoryManager = new TransactionCategoryManager();
            ICRUDRepository<Transaction> _transactionManager = new TransactionManager();
            _users = _userManager.GetAll();
            _transactionCategories = _transactionCategoryManager.GetAll();
            Transactions = _transactionManager.GetAll();
            Balance = 0;
            //SetValues();
        }
        public void SetValues()
        {
            Dictionary<string, UserTransactionsViewModel> usersTransactions = new Dictionary<string, UserTransactionsViewModel>();
            foreach (var user in _users.Select(z=>z.Name))
            {
                usersTransactions.Add(user, new UserTransactionsViewModel {UserName = user, Expenses = 0, Income = 0});
            }
            Dictionary<string, TransactionCategoryViewModel> expenseCategories = new Dictionary<string, TransactionCategoryViewModel>();
            Dictionary<string, TransactionCategoryViewModel> incomeCategories = new Dictionary<string, TransactionCategoryViewModel>();
            foreach (var transactionCategory in _transactionCategories)
            {
                if (transactionCategory.TransactionType == ETransactionType.Expense)
                {
                    expenseCategories.Add(transactionCategory.TransactionCategoryName,
                                          new TransactionCategoryViewModel
                                          { TransactionCategoryName= transactionCategory.TransactionCategoryName,
                                            Amount = 0});
                }
                else
                {
                    incomeCategories.Add(transactionCategory.TransactionCategoryName,
                                          new TransactionCategoryViewModel
                                          { TransactionCategoryName = transactionCategory.TransactionCategoryName,
                                            Amount = 0 }
                                          );
                }
            }

            foreach (var transaction in Transactions.Where(z => z.TransactionDate >= StartTime && z.TransactionDate < EndTime))
            {
                if (transaction.TransactionCategory.TransactionType == ETransactionType.Expense)
                {
                    usersTransactions[transaction.User.Name].Expenses += transaction.Sum;
                    expenseCategories[transaction.TransactionCategory.TransactionCategoryName].Amount += transaction.Sum;
                    Balance -= transaction.Sum;
                }
                else
                {
                    usersTransactions[transaction.User.Name].Income += transaction.Sum;
                    incomeCategories[transaction.TransactionCategory.TransactionCategoryName].Amount += transaction.Sum;
                    Balance += transaction.Sum;
                }
            }
            UsersExpensesIncomes = usersTransactions.Values.ToList();
            IncomesByCategory = incomeCategories.Values.ToList();
            ExpensesByCategory = expenseCategories.Values.ToList();
        }
    }
}
