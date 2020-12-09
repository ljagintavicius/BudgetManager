using BudgetManager.BL;
using BudgetManager.DL;
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
    /// Interaction logic for MenuUserControl.xaml
    /// </summary>
    public partial class MenuUserControl : UserControl
    {
        public event RoutedEventHandler btnShowExpensesIncome_ClickHandler;
        public event RoutedEventHandler btnLogOut_ClickHandler;
        public event RoutedEventHandler btnAddExpenseIncome_ClickHandler;
        private readonly LoginWindow _loginWindow;
        private readonly IUserManager _userManager;

        public User SelectedUser { get; set; }
        public MenuUserControl()
        {
            InitializeComponent();
            _loginWindow = new LoginWindow();
            _userManager = new UserManager();
            btnShowExpensesIncome.IsEnabled = false;
            btnAddExpenseIncome.IsEnabled = false;
            btnEditExpensesIncome.IsEnabled = false;
            btnShowReport.IsEnabled = false;
            btnLogOut.IsEnabled = false;

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            btnLogin.IsEnabled = false;
            if (_loginWindow.IsActive) _loginWindow.Visibility = Visibility.Visible;
            else _loginWindow.Show();
            _loginWindow.btnLoginWindowLogin_ClickHandler += UnlockOtherButtons;

        }
        private void UnlockOtherButtons(object sender, RoutedEventArgs e)
        {
            _loginWindow.Visibility = Visibility.Hidden;
            SelectedUser = (User)sender;
            txtLoggedAs.Text = $"Logged as: {SelectedUser.Name}";
            btnShowExpensesIncome.IsEnabled = true;
            btnAddExpenseIncome.IsEnabled = true;
            btnEditExpensesIncome.IsEnabled = true;
            btnShowReport.IsEnabled = true;
            btnLogOut.IsEnabled = true;
            
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            txtLoggedAs.Text = string.Empty;
            btnLogin.IsEnabled = true;
            btnShowExpensesIncome.IsEnabled = false;
            btnAddExpenseIncome.IsEnabled = false;
            btnEditExpensesIncome.IsEnabled = false;
            btnShowReport.IsEnabled = false;
            btnLogOut.IsEnabled = false;
            btnLogOut_ClickHandler(sender, e);
        }

        private void btnShowExpensesIncome_Click(object sender, RoutedEventArgs e)
        {
            btnShowExpensesIncome_ClickHandler(sender, e);
        }

        private void btnAddExpenseIncome_Click(object sender, RoutedEventArgs e)
        {
            btnAddExpenseIncome_ClickHandler(SelectedUser, e);
        }
    }
}
