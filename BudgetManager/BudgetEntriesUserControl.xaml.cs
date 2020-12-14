using BudgetManager.BL;
using BudgetManager.BL.Services;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using BudgetManager.DL;
using System.Linq;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for ViewEditExpenseIncomeUserControl.xaml
    /// </summary>
    public partial class BudgetEntriesUserControl : UserControl
    {
        public event RoutedEventHandler btnAdd_ClickHandler;
        private List<TransactionViewModel> _transactionViewModels;
        private ITransactionViewModelManager _transactionViewModelManager;
        public User SelectedUser { get; set; }
        public BudgetEntriesUserControl()
        {
            InitializeComponent();
            AddExpenseIncomeUserControl.Visibility = Visibility.Hidden;
            AddExpenseIncomeUserControl.btnSave_ClickHandler += ShowDataGrid;
            
        }

        public void ShowDataGrid(object sender, RoutedEventArgs e)
        {
            _transactionViewModelManager = new TransactionViewModelManager();
            _transactionViewModels = _transactionViewModelManager.GetAllOrUpdate();
            dgBudget.ItemsSource = _transactionViewModels;
            AddExpenseIncomeUserControl.Visibility = Visibility.Hidden;
            dgBudget.ScrollIntoView(dgBudget.Items.GetItemAt(dgBudget.Items.Count-1));
            
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddExpenseIncomeUserControl.Visibility = Visibility.Visible;
            AddExpenseIncomeUserControl.SelectedUser = this.SelectedUser;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
