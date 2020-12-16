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
    /// Interaction logic for ShowSummaryUserControl.xaml
    /// </summary>
    public partial class ShowSummaryUserControl : UserControl
    {
        const int oneMonth = 1;
        private List<Transaction> _transactions;
        private SummaryViewModelManager _summaryViewModelManager;

        public ShowSummaryUserControl()
        {
            InitializeComponent();
            HideTables();
        }

        private void cmbSelectedYearAndMonth_DropDownOpened(object sender, EventArgs e)
        {
            _summaryViewModelManager = new SummaryViewModelManager();
            _transactions = _summaryViewModelManager.Transactions;
            List<DateTime> uniqueMonthsYears = _transactions.Select(z => z.TransactionDate)
                .Select(d => new DateTime(d.Year, d.Month, 1))
                .Distinct()
                .OrderByDescending(z => z.Date)
                .ToList();
            cmbSelectedYearAndMonth.ItemsSource = uniqueMonthsYears.Select(z => z.ToString("yyyy-MM")).ToList();
        }

        private void cmbSelectedYearAndMonth_DropDownClosed(object sender, EventArgs e)
        {
            ShowSummary();
        }

        public void ShowSummary()
        {
            if (cmbSelectedYearAndMonth.SelectedItem != null)
            {
                _summaryViewModelManager.StartTime = DateTime.Parse((string)cmbSelectedYearAndMonth.SelectedItem);
                _summaryViewModelManager.EndTime = DateTime.Parse((string)cmbSelectedYearAndMonth.SelectedItem).AddMonths(oneMonth);
                ShowTables();
                _summaryViewModelManager.SetValues();
                lvUsersInfo.ItemsSource = _summaryViewModelManager.UsersExpensesIncomes;
                txtTotalBalance.Text = $"{_summaryViewModelManager.Balance} Eur";
                txtTotalExpenses.Text = $"{_summaryViewModelManager.TotalExpenses} Eur";
                txtTotalIncome.Text = $"{_summaryViewModelManager.TotalIncome} Eur";
                lvExpenseCategories.ItemsSource = _summaryViewModelManager.ExpensesByCategory;
                lvIncomeCategories.ItemsSource = _summaryViewModelManager.IncomesByCategory;
            }
        }
        private void ShowTables()
        {
            spInfo.Visibility = Visibility.Visible;
            lvUsersInfo.Visibility = Visibility.Visible;
            lvExpenseCategories.Visibility = Visibility.Visible;
            lvIncomeCategories.Visibility = Visibility.Visible;
        }

        public void HideTables()
        {
            spInfo.Visibility = Visibility.Hidden;
            lvUsersInfo.Visibility = Visibility.Hidden;
            lvExpenseCategories.Visibility = Visibility.Hidden;
            lvIncomeCategories.Visibility = Visibility.Hidden;
            cmbSelectedYearAndMonth.SelectedItem = null;
        }
    }
}
