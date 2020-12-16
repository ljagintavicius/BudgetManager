using BudgetManager.BL;
using BudgetManager.BL.Services;
using BudgetManager.DL;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for ViewEditExpenseIncomeUserControl.xaml
    /// </summary>
    public partial class BudgetEntriesUserControl : UserControl
    {
        private List<TransactionViewModel> _transactionViewModels;
        private ITransactionViewModelManager _transactionViewModelManager;
        private TransactionManager _transactionManager;
        public User SelectedUser { get; set; }
        public BudgetEntriesUserControl()
        {
            InitializeComponent();
            AddEntryUserControl.Visibility = Visibility.Hidden;
            _transactionManager = new TransactionManager();
            AddEntryUserControl.btnSave_ClickHandler += ShowDataGrid;
            AddEntryUserControl.btnCancel_ClickHandler += ShowDataGrid;
            EditDeleteUserControl.btnSaveChanges_ClickHandler += ShowDataGrid;
            EditDeleteUserControl.btnDelete_ClickHandler += ShowDataGrid;
            EditDeleteUserControl.btnCancel_ClickHandler += ShowDataGrid;

        }

        public void ShowDataGrid(object sender, RoutedEventArgs e)
        {
            _transactionViewModelManager = new TransactionViewModelManager();
            _transactionViewModels = _transactionViewModelManager.GetAll();
            dgBudget.ItemsSource = _transactionViewModels;
            AddEntryUserControl.Visibility = Visibility.Hidden;
            EditDeleteUserControl.Visibility = Visibility.Hidden;
            txtEditDeleteMessage.Visibility = Visibility.Hidden;
            spButtonsPanel.Visibility = Visibility.Visible;
            if (dgBudget.Items.Count > 0) dgBudget.ScrollIntoView(dgBudget.Items.GetItemAt(dgBudget.Items.Count - 1));


        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEntryUserControl.Visibility = Visibility.Visible;
            EditDeleteUserControl.Visibility = Visibility.Hidden;
            AddEntryUserControl.SelectedUser = this.SelectedUser;
        }

        private void btnEditDelete_Click(object sender, RoutedEventArgs e)
        {

            AddEntryUserControl.Visibility = Visibility.Hidden;
            EditDeleteUserControl.Visibility = Visibility.Visible;
            spButtonsPanel.Visibility = Visibility.Hidden;
            txtEditDeleteMessage.Visibility = Visibility.Visible;
        }

        private void dgBudget_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgBudget.SelectedItem != null)
            {
                EditDeleteUserControl.SelectedTransaction = _transactionManager.Get(((TransactionViewModel)dgBudget.SelectedItem).TransactionId);
                EditDeleteUserControl.SetValues();
            }
        }
    }
}
