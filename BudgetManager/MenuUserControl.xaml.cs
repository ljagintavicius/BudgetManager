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
        private readonly LoginWindow _loginWindow;

        public User SelectedUser { get; set; }
        public MenuUserControl()
        {
            InitializeComponent();
            _loginWindow = new LoginWindow();
            LockInterfaceButtons();
            
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            btnLogin.IsEnabled = false;
            if (_loginWindow.IsActive) _loginWindow.Visibility = Visibility.Visible;
            else _loginWindow.Show();
            _loginWindow.btnLoginWindowLogin_ClickHandler += UserLogin;

        }
        private void LockInterfaceButtons()
        {
            btnBudgetEntries.IsEnabled = false;
            btnShowSummary.IsEnabled = false;
            btnLogOut.IsEnabled = false;
        }
        private void UnlockInterfaceButtons()
        {
            btnBudgetEntries.IsEnabled = true;
            btnShowSummary.IsEnabled = true;
            btnLogOut.IsEnabled = true;
        }
        private void UserLogin(object sender, RoutedEventArgs e)
        {
            _loginWindow.Visibility = Visibility.Hidden;
            SelectedUser = _loginWindow.SelectedUser;
            txtLoggedAs.Text = $"Logged as: {SelectedUser.Name}";
            UnlockInterfaceButtons();

        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            txtLoggedAs.Text = string.Empty;
            btnLogin.IsEnabled = true;
            LockInterfaceButtons();
            btnLogOut_ClickHandler(sender, e);
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
