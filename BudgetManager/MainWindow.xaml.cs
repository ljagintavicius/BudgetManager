using BudgetManager.BL;
using BudgetManager.BL.Services;
using BudgetManager.DL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TransactionViewModel> _transactionViewModels;
        private ITransactionViewModelManager _transactionViewModelManager;

        public MainWindow()
        {
            InitializeComponent();
            _transactionViewModelManager = new TransactionViewModelManager();
            dgBudget.Visibility = Visibility.Hidden;
            AddExpenseIncomeUserControl.Visibility = Visibility.Hidden;
            ShowSummaryUserControl.Visibility = Visibility.Hidden;
            MenuUserControl.btnShowExpensesIncome_ClickHandler += btnShowExpensesIncome_Click;
            MenuUserControl.btnLogOut_ClickHandler += btnLogOut_Click;
            MenuUserControl.btnAddExpenseIncome_ClickHandler += btnAddExpenseIncome_Click;
            MenuUserControl.btnShowSummary_ClickHandler += btnShowSummary_Click;
        }

        private void btnShowExpensesIncome_Click(object sender, RoutedEventArgs e)
        {
            _transactionViewModels = _transactionViewModelManager.GetAllOrUpdate();
            dgBudget.ItemsSource = _transactionViewModels;
            dgBudget.Visibility = Visibility.Visible;
            AddExpenseIncomeUserControl.Visibility = Visibility.Hidden;
            ShowSummaryUserControl.Visibility = Visibility.Hidden;
        }
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            dgBudget.Visibility = Visibility.Hidden;
            AddExpenseIncomeUserControl.Visibility = Visibility.Hidden;
            ShowSummaryUserControl.Visibility = Visibility.Hidden;
        }
        private void btnAddExpenseIncome_Click(object sender, RoutedEventArgs e)
        {
            dgBudget.Visibility = Visibility.Hidden;
            AddExpenseIncomeUserControl.Visibility = Visibility.Visible;
            ShowSummaryUserControl.Visibility = Visibility.Hidden;
            AddExpenseIncomeUserControl.SelectedUser = MenuUserControl.SelectedUser;

        }
        private void btnShowSummary_Click(object sender, RoutedEventArgs e)
        {
            dgBudget.Visibility = Visibility.Hidden;
            AddExpenseIncomeUserControl.Visibility = Visibility.Hidden;
            ShowSummaryUserControl.Visibility = Visibility.Visible;
        }

    }
}
