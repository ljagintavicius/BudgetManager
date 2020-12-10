using BudgetManager.BL;
using BudgetManager.BL.Services;
using BudgetManager.DL;
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
    /// Interaction logic for AddExpenseIncomeUserControl.xaml
    /// </summary>
    public partial class AddExpenseIncomeUserControl : UserControl
    {
        private DateTime _selectedDateTime;
        private readonly List<TransactionCategory> _transactionCategories;
        private readonly ICRUDRepository<Transaction> _transactionManager;
        private readonly ITransactionCategoryManager _transactionCategoryManager;
        private Transaction _transaction;

        public User SelectedUser { get; set; }
        public TransactionCategory SelectedTransactionCategory { get; set; }
        public AddExpenseIncomeUserControl()
        {
            InitializeComponent();
            
            _selectedDateTime = DateTime.Now;
            txtDate.Text = _selectedDateTime.ToString("yyyy-MM-dd");
            cmbCategory.IsEnabled = false;
            _transactionManager = new TransactionManager();
            _transactionCategoryManager = new TransactionCategoryManager();
            _transactionCategories = _transactionCategoryManager.GetAll();
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
                    TransactionCategoryId = SelectedTransactionCategory.TransactionCategoryId,
                    TransactionDate = dateTime,
                    Sum = amount
                };
                _transactionManager.Add(_transaction);
                MessageBox.Show("Entry saved!");
                cmbExpenseOrIncome.SelectedItem = null;
                cmbCategory.SelectedItem = null;
                txtAmount.Text = string.Empty;
            }
            else MessageBox.Show("Incorrect input!");
        }

        private void cmbExpenseOrIncome_DropDownClosed(object sender, EventArgs e)
        {
            cmbCategory.IsEnabled = true;
            if ((string)cmbExpenseOrIncome.SelectedItem == Enum.GetName(typeof(ETransactionType),ETransactionType.Expense))
            {
                cmbCategory.ItemsSource = _transactionCategories.Where(z => z.TransactionType == ETransactionType.Expense).Select(z=>z.TransactionCategoryName);
            }
            else
            {
                cmbCategory.ItemsSource = _transactionCategories.Where(z => z.TransactionType == ETransactionType.Income).Select(z => z.TransactionCategoryName);
            }

        }

        private void cmbCategory_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedIndex > -1)
                SelectedTransactionCategory = _transactionCategoryManager.SelectTransactionCategoryrByName(cmbCategory.SelectedItem.ToString());
        }
    }
}
