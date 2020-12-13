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
    public class UserTransactionsViewModelManager
    {
        private readonly List<User> _users;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Balance { get; set; }

        public UserTransactionsViewModelManager(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
            IUserManager _userManager = new UserManager();
            _users = _userManager.GetAll();
        }

        public List <UserTransactionsViewModel> GetDateSortedUserTransactions()
        {
            List<UserTransactionsViewModel> userTransactionsViewModels = new List<UserTransactionsViewModel>();
            foreach (var user in _users)
            {
                userTransactionsViewModels.Add(new UserTransactionsViewModel()
                {
                    UserName = user.Name,
                    Income = user
                            .Transactions
                            .Where(z => z.TransactionDate >= StartTime && z.TransactionDate < EndTime)
                            .Where(z => z.TransactionCategory.TransactionType == ETransactionType.Income)
                            .Sum(z => z.Sum),
                    Expenses = user
                            .Transactions
                            .Where(z => z.TransactionDate >= StartTime && z.TransactionDate < EndTime)
                            .Where(z => z.TransactionCategory.TransactionType == ETransactionType.Expense)
                            .Sum(z => z.Sum)
                });
            }
            userTransactionsViewModels.Add(GetTotalIncomeExpensesSum());
            return userTransactionsViewModels;
        }

        private UserTransactionsViewModel GetTotalIncomeExpensesSum ()
        {
            UserTransactionsViewModel totalIncomeExpensesSum = new UserTransactionsViewModel()
            {
                UserName = "Total",
                Income = _users
                            .Select(x => x.Transactions
                            .Where(z => z.TransactionDate >= StartTime && z.TransactionDate < EndTime)
                            .Where(z => z.TransactionCategory.TransactionType == ETransactionType.Income)
                            .Sum(z => z.Sum)).Sum(),
                Expenses = _users
                            .Select(x => x.Transactions
                            .Where(z => z.TransactionDate >= StartTime && z.TransactionDate < EndTime)
                            .Where(z => z.TransactionCategory.TransactionType == ETransactionType.Expense)
                            .Sum(z => z.Sum)).Sum()
            };
            Balance = totalIncomeExpensesSum.Income - totalIncomeExpensesSum.Expenses;
            return totalIncomeExpensesSum;
        }
    }
}
