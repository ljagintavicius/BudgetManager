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
        public ShowSummaryUserControl()
        {
            InitializeComponent();
            _transactionManager = new TransactionManager();
            txtBalanceText.Visibility = Visibility.Hidden;
            lvUsersInfo.Visibility = Visibility.Hidden;
            
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
                lvUsersInfo.Visibility = Visibility.Visible;
                lvUsersInfo.ItemsSource = _userTransactonsViewModels;
                txtBalanceText.Visibility = Visibility.Visible;
                txtTotalBalance.Text = $"{_userTransactionsViewModelManager.Balance} Eur";
            }

        }
    }
}
