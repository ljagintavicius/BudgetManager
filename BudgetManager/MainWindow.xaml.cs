using BudgetManager.BL;
using BudgetManager.BL.Services;
using BudgetManager.DL;
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
        private readonly List<Transaction> _transactions;
        private readonly ICRUDRepository<Transaction> _transactionManager;
        public MainWindow()
        {
            InitializeComponent();
            _transactionManager = new TransactionManager();
            _transactions = _transactionManager.GetAll();
            dgBudget.ItemsSource = _transactions;
            dgBudget.Visibility = Visibility.Hidden;
            MenuUserControl.btnShowExpensesIncome_ClickHandler += btnShowExpensesIncome_Click;
            MenuUserControl.btnLogOut_ClickHandler += btnLogOut_Click;
            MenuUserControl.btnAddExpenseIncome_ClickHandler += btnAddExpenseIncome_Click;
        }

        private void btnShowExpensesIncome_Click(object sender, RoutedEventArgs e)
        {
            dgBudget.Visibility = Visibility.Visible;
        }
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            dgBudget.Visibility = Visibility.Hidden;
        }
        private void btnAddExpenseIncome_Click(object sender, RoutedEventArgs e)
        {
            dgBudget.Visibility = Visibility.Hidden;

        }
    }
}
