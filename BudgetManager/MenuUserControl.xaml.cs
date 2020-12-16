using BudgetManager.DL;
using System.Windows;
using System.Windows.Controls;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for MenuUserControl.xaml
    /// </summary>
    public partial class MenuUserControl : UserControl
    {
        public event RoutedEventHandler btnBudgetEntries_ClickHandler;
        public event RoutedEventHandler btnLogOut_ClickHandler;
        public event RoutedEventHandler btnShowSummary_ClickHandler;
        private LoginWindow _loginWindow;
        private bool _isUserLoggedIn;
        public User SelectedUser { get; set; }
        public MenuUserControl()
        {
            InitializeComponent();
            _loginWindow = new LoginWindow();
            _isUserLoggedIn = false;
            LockInterfaceButtons();
        }

        private void btnLoginLogout_Click(object sender, RoutedEventArgs e)
        {
            if (!_isUserLoggedIn)
            {
                if (_loginWindow.IsActive) _loginWindow.Visibility = Visibility.Visible;
                else _loginWindow.Show();
                _loginWindow.btnLoginWindowLogin_ClickHandler += UserLoggedIn;
            }
            else
            {
                _isUserLoggedIn = false;
                btnLoginLogout.Content = "Log in";
                txtLoggedAs.Text = string.Empty;
                LockInterfaceButtons();
                btnLogOut_ClickHandler(sender, e);
            }
        }
        private void LockInterfaceButtons()
        {
            btnBudgetEntries.IsEnabled = false;
            btnShowSummary.IsEnabled = false;
        }
        private void UnlockInterfaceButtons()
        {
            btnBudgetEntries.IsEnabled = true;
            btnShowSummary.IsEnabled = true;
        }
        private void UserLoggedIn(object sender, RoutedEventArgs e)
        {
            btnLoginLogout.Content = "Log out";
            _isUserLoggedIn = true;
            _loginWindow.Visibility = Visibility.Hidden;
            SelectedUser = _loginWindow.SelectedUser;
            txtLoggedAs.Text = $"Logged as: {SelectedUser.Name}";
            UnlockInterfaceButtons();
        }

        private void btnBudgetEntries_Click(object sender, RoutedEventArgs e)
        {
            btnBudgetEntries_ClickHandler(sender, e);
        }

        private void btnShowSummary_Click(object sender, RoutedEventArgs e)
        {
            btnShowSummary_ClickHandler(sender, e);

        }
    }
}
