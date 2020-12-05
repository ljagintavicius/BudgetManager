using BudgetManager.DL;
using BudgetManager.DL.Models;
using System.Linq;
using System.Windows;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (var context = new BudgetContext())
            {
                var kazkas = context.TransactionCategories.FirstOrDefault(z => z.TransactionType == ETransactionType.Expense);
            }
        }
    }
}
