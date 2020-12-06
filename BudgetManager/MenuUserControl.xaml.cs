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
        private readonly LoginWindow _loginWindow;
        private User _selectedUser;
        private readonly IUserManager _userManager;
        public MenuUserControl()
        {
            InitializeComponent();
            _loginWindow = new LoginWindow();
            _userManager = new UserManager();

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            btnLogin.IsEnabled = false;
            _loginWindow.Show();
            _loginWindow.btnLoginWindowLogin_ClickHandler += UnlockOtherButtons;

        }
        private void UnlockOtherButtons(object sender, RoutedEventArgs e)
        {
            _loginWindow.Close();
            _selectedUser = _userManager.SelectUserByName((string)sender);
            txtLoggedAs.Text = $"Logged as: {_selectedUser.Name}";
        }
    }
}
