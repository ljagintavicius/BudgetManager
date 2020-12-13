using BudgetManager.BL;
using BudgetManager.BL.Models;
using BudgetManager.BL.Services;
using BudgetManager.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for ShowSummaryUserControl.xaml
    /// </summary>
    public partial class ShowSummaryUserControl : UserControl
    {
        const int oneMonth = 1;
        private List<Transaction> _transactions;
        private ICRUDRepository<Transaction> _transactionManager;
        private List<UserTransactionsViewModel> _userTransactonsViewModels;
        private UserTransactionsViewModelManager _userTransactionsViewModelManager;
        private List<TransactionCategoryViewModel> _expenseCategoryViewModels;
        private List<TransactionCategoryViewModel> _incomeCategoryViewModels;
        private TransactionCategoryViewModelManager _transactionCategoryViewModelManager;
        public ShowSummaryUserControl()
        {
            InitializeComponent();
            _transactionManager = new TransactionManager();
            HideListViews();
        }

        private void cmbSelectedYearAndMonth_DropDownOpened(object sender, EventArgs e)
        {
            _transactions = _transactionManager.GetAll();
            List<DateTime> uniqueMonthsYears = _transactions.Select(z => z.TransactionDate)
                .Select(d => new DateTime(d.Year, d.Month, 1))
                .Distinct()
                .ToList();
            cmbSelectedYearAndMonth.ItemsSource = uniqueMonthsYears.Select(z=>z.ToString("yyyy-MM")).ToList();
        }

        private void cmbSelectedYearAndMonth_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbSelectedYearAndMonth.SelectedItem != null)
            {
                _userTransactionsViewModelManager
                  = new UserTransactionsViewModelManager(DateTime.Parse((string)cmbSelectedYearAndMonth.SelectedItem), DateTime.Parse((string)cmbSelectedYearAndMonth.SelectedItem).AddMonths(oneMonth));
                _userTransactonsViewModels = _userTransactionsViewModelManager.GetDateSortedUserTransactions();
                ShowListViews();
                lvUsersInfo.ItemsSource = _userTransactonsViewModels;
                txtTotalBalance.Text = $"{_userTransactionsViewModelManager.Balance} Eur";
                _transactionCategoryViewModelManager
                    = new TransactionCategoryViewModelManager(DateTime.Parse((string)cmbSelectedYearAndMonth.SelectedItem), DateTime.Parse((string)cmbSelectedYearAndMonth.SelectedItem).AddMonths(oneMonth));
                _expenseCategoryViewModels = _transactionCategoryViewModelManager.GetDateSortedTransactionCategoriesAndSum(ETransactionType.Expense);
                _incomeCategoryViewModels = _transactionCategoryViewModelManager.GetDateSortedTransactionCategoriesAndSum(ETransactionType.Income);
                lvExpenseCategories.ItemsSource = _expenseCategoryViewModels;
                lvIncomeCategories.ItemsSource = _incomeCategoryViewModels;
            }

        }
        private void ShowListViews()
        {
            lvUsersInfo.Visibility = Visibility.Visible;
            txtBalanceText.Visibility = Visibility.Visible;
            lvExpenseCategories.Visibility = Visibility.Visible;
            lvIncomeCategories.Visibility = Visibility.Visible;
        }

        private void HideListViews()
        {
            txtBalanceText.Visibility = Visibility.Hidden;
            lvUsersInfo.Visibility = Visibility.Hidden;
            lvExpenseCategories.Visibility = Visibility.Hidden;
            lvIncomeCategories.Visibility = Visibility.Hidden;
        }
    }
}
