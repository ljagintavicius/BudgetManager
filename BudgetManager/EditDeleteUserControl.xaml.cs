using BudgetManager.BL;
using BudgetManager.BL.Services;
using BudgetManager.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for EditEntryUserControl.xaml
    /// </summary>
    public partial class EditDeleteUserControl : UserControl
    {
        private List<TransactionCategory> _transactionCategories;
        private ITransactionCategoryManager _transactionCategoryManager;
        private TransactionCategory _selectedTransactionCategory;
        private Transaction _transaction;
        private ICRUDRepository<Transaction> _transactionManager;

        public event RoutedEventHandler btnSaveChanges_ClickHandler;
        public event RoutedEventHandler btnDelete_ClickHandler;
        public event RoutedEventHandler btnCancel_ClickHandler;

        public TransactionViewModel SelectedTransactionViewModel { get; set; }
        public Transaction SelectedTransaction { get; set; }
        public EditDeleteUserControl()
        {
            InitializeComponent();
            _transactionCategoryManager = new TransactionCategoryManager();
            _transactionManager = new TransactionManager();
        }

        public void SetValues()
        {
            _transactionCategories = _transactionCategoryManager.GetAll();
            cmbExpenseOrIncome.ItemsSource = Enum.GetNames(typeof(ETransactionType));
            cmbCategory.ItemsSource = _transactionCategories.Select(z => z.TransactionCategoryName).ToList();
            txtDate.Text = SelectedTransaction.TransactionDate.ToString("yyyy-MM-dd");
            cmbExpenseOrIncome.SelectedItem = Enum.GetName(typeof(ETransactionType), SelectedTransaction.TransactionCategory.TransactionType);
            cmbCategory.SelectedItem = SelectedTransaction.TransactionCategory.TransactionCategoryName;
            txtAmount.Text = SelectedTransaction.Sum.ToString();
        }




        private void cmbExpenseOrIncome_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedIndex > -1)
                _selectedTransactionCategory = _transactionCategoryManager.SelectTransactionCategoryByName(cmbCategory.SelectedItem.ToString());

        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParse(txtDate.Text, out DateTime dateTime) &&
                cmbExpenseOrIncome.SelectedItem != null &&
                cmbCategory.SelectedItem != null &&
                decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                _transaction = new Transaction
                {
                    TransactionId = SelectedTransaction.TransactionId,
                    TransactionCategoryId = _transactionCategoryManager.SelectTransactionCategoryByName(cmbCategory.SelectedItem.ToString()).TransactionCategoryId,
                    UserId = SelectedTransaction.UserId,
                    TransactionDate = dateTime,
                    Sum = amount
                };
                _transactionManager.Update(_transaction);
                MessageBox.Show("Entry updated!");
                cmbExpenseOrIncome.SelectedItem = null;
                cmbCategory.SelectedItem = null;
                txtAmount.Text = string.Empty;
                btnSaveChanges_ClickHandler(sender, e);
            }
            else MessageBox.Show("Incorrect input!");
        }


        private void cmbExpenseOrIncome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((string)cmbExpenseOrIncome.SelectedItem == Enum.GetName(typeof(ETransactionType), ETransactionType.Expense))
            {
                cmbCategory.ItemsSource = _transactionCategories.Where(z => z.TransactionType == ETransactionType.Expense).Select(z => z.TransactionCategoryName);
            }
            else
            {
                cmbCategory.ItemsSource = _transactionCategories.Where(z => z.TransactionType == ETransactionType.Income).Select(z => z.TransactionCategoryName);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTransaction == null) MessageBox.Show("Entry was not selected!");
            else if (MessageBox.Show("Delete entry?", "Question", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _transactionManager.Delete(SelectedTransaction.TransactionId);
                btnDelete_ClickHandler(sender, e);
            }
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            btnCancel_ClickHandler(sender, e);
        }
    }
}
