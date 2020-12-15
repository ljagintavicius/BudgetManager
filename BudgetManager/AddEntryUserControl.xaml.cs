using BudgetManager.BL;
using BudgetManager.BL.Services;
using BudgetManager.DL;
using BudgetManager.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for AddExpenseIncomeUserControl.xaml
    /// </summary>
    public partial class AddEntryUserControl : UserControl
    {
        public event RoutedEventHandler btnSave_ClickHandler;
        public event RoutedEventHandler btnCancel_ClickHandler;
        private DateTime _selectedDateTime;
        private List<TransactionCategory> _transactionCategories;
        private readonly ICRUDRepository<Transaction> _transactionManager;
        private readonly ITransactionCategoryManager _transactionCategoryManager;
        private Transaction _transaction;
        private TransactionCategory _selectedTransactionCategory;

        public User SelectedUser { get; set; }

        public AddEntryUserControl()
        {
            InitializeComponent();
            _selectedDateTime = DateTime.Now;
            txtDate.Text = _selectedDateTime.ToString("yyyy-MM-dd");
            cmbCategory.IsEnabled = false;
            _transactionManager = new TransactionManager();
            _transactionCategoryManager = new TransactionCategoryManager();
            cmbExpenseOrIncome.ItemsSource = Enum.GetNames(typeof(ETransactionType));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParse(txtDate.Text, out DateTime dateTime) &&
                cmbExpenseOrIncome.SelectedItem != null &&
                cmbCategory.SelectedItem != null &&
                decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                _transaction = new Transaction
                {
                    UserId = SelectedUser.UserId,
                    TransactionCategoryId = _selectedTransactionCategory.TransactionCategoryId,
                    TransactionDate = dateTime,
                    Sum = amount
                };
                _transactionManager.Add(_transaction);
                MessageBox.Show("Entry saved!");
                cmbExpenseOrIncome.SelectedItem = null;
                cmbCategory.SelectedItem = null;
                txtAmount.Text = string.Empty;
                cmbCategory.IsEnabled = false;
                btnSave_ClickHandler(sender, e);
            }
            else MessageBox.Show("Incorrect input!");

        }

        private void cmbExpenseOrIncome_DropDownClosed(object sender, EventArgs e)
        {
            _transactionCategories = _transactionCategoryManager.GetAll();
            cmbCategory.IsEnabled = true;
            if ((string)cmbExpenseOrIncome.SelectedItem == Enum.GetName(typeof(ETransactionType), ETransactionType.Expense))
            {
                cmbCategory.ItemsSource = _transactionCategories.Where(z => z.TransactionType == ETransactionType.Expense).Select(z => z.TransactionCategoryName);
            }
            else
            {
                cmbCategory.ItemsSource = _transactionCategories.Where(z => z.TransactionType == ETransactionType.Income).Select(z => z.TransactionCategoryName);
            }

        }

        private void cmbCategory_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedItem != null)
                _selectedTransactionCategory = _transactionCategoryManager.SelectTransactionCategoryByName(cmbCategory.SelectedItem.ToString());

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            btnCancel_ClickHandler(sender, e);
        }
    }
}
