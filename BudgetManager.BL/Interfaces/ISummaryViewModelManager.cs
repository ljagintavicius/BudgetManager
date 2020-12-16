using BudgetManager.BL.Models;
using BudgetManager.DL.Models;
using System;
using System.Collections.Generic;

namespace BudgetManager.BL.Services
{
    public interface ISummaryViewModelManager
    {
        decimal Balance { get; set; }
        DateTime EndTime { get; set; }
        List<TransactionCategoryViewModel> ExpensesByCategory { get; set; }
        List<TransactionCategoryViewModel> IncomesByCategory { get; set; }
        DateTime StartTime { get; set; }
        decimal TotalExpenses { get; set; }
        decimal TotalIncome { get; set; }
        List<Transaction> Transactions { get; set; }
        List<UserTransactionsViewModel> UsersExpensesIncomes { get; set; }

        void PrepareSummary();
    }
}