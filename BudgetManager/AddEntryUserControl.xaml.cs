﻿using BudgetManager.BL;
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
        private List<TransactionCategory> _transactionCategories;
        private readonly ICRUDRepository<Transaction> _transactionManager;
        private readonly ITransactionCategoryManager _transactionCategoryManager;
        private TransactionCategory _selectedTransactionCategory;

        public User SelectedUser { get; set; }

        public AddEntryUserControl()
        {
            InitializeComponent();
            dpDate.SelectedDate = DateTime.Now;
            cmbCategory.IsEnabled = false;
            _transactionManager = new TransactionManager();
            _transactionCategoryManager = new TransactionCategoryManager();
            cmbExpenseOrIncome.ItemsSource = Enum.GetNames(typeof(ETransactionType));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (dpDate.SelectedDate.HasValue &&
                cmbExpenseOrIncome.SelectedItem != null &&
                cmbCategory.SelectedItem != null &&
                decimal.TryParse(txtAmount.Text, out decimal amount) &&
                amount > 0
                )
            {
                Transaction newTransaction = new Transaction
                {
                    UserId = SelectedUser.UserId,
                    TransactionCategoryId = _selectedTransactionCategory.TransactionCategoryId,
                    TransactionDate = dpDate.SelectedDate.Value,
                    Sum = amount
                };
                _transactionManager.Add(newTransaction);
                MessageBox.Show("Entry saved!");
                cmbExpenseOrIncome.SelectedItem = null;
                cmbCategory.SelectedItem = null;
                txtAmount.Text = string.Empty;
                cmbCategory.IsEnabled = false;
                btnSave_ClickHandler(sender, e);
            }
            else if (cmbExpenseOrIncome.SelectedItem == null) MessageBox.Show("Expense/Income was not selected!");
            else if (cmbCategory.SelectedItem == null) MessageBox.Show("Category was not selected!");
            else if (!decimal.TryParse(txtAmount.Text, out amount)) MessageBox.Show("Amount must be decimal number!");
            else if (amount <= 0) MessageBox.Show("Amount must be greater than 0!");
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
                _selectedTransactionCategory = _transactionCategoryManager.GetByName(cmbCategory.SelectedItem.ToString());

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            btnCancel_ClickHandler(sender, e);
        }
    }
}
