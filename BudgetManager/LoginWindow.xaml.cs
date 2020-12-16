using BudgetManager.BL;
using BudgetManager.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public event RoutedEventHandler btnLoginWindowLogin_ClickHandler;
        private List<User> _usersList;
        public User SelectedUser { get; set; }
        private readonly IUserManager _userManager;

        public LoginWindow()
        {
            InitializeComponent();
            _userManager = new UserManager();
            rbSelectUser.IsChecked = true;
        }



        private void btnNewUserCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewUser.Text) && txtNewUser.Text.Length < 20)
            {
                _userManager.Add(new User { Name = txtNewUser.Text });
                MessageBox.Show($"User {txtNewUser.Text} created");
            }
            else if (txtNewUser.Text.Length >= 20) MessageBox.Show("User name can't be longer than 20 characters!");
            else MessageBox.Show("User name can't be empty!");


            txtNewUser.Text = string.Empty;
        }


        private void cmbSelectUser_Dropdown(object sender, EventArgs e)
        {
            _usersList = _userManager.GetAll();
            cmbSelectUser.ItemsSource = _usersList.Select(z => z.Name);
        }

        private void btnLoginWindowLogin_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSelectUser.SelectedItem != null)
            {
                SelectedUser = _userManager.GetByName(cmbSelectUser.SelectedItem.ToString());
                btnLoginWindowLogin_ClickHandler(sender, e);
            }
            else
            {
                MessageBox.Show("User was not selected!");
            }
            cmbSelectUser.SelectedItem = null;
        }


        private void loginWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void rbSelectUser_Checked(object sender, RoutedEventArgs e)
        {
            txtSelectUser.Visibility = Visibility.Visible;
            cmbSelectUser.Visibility = Visibility.Visible;
            btnLoginWindowLogin.Visibility = Visibility.Visible;
            txtCreateUser.Visibility = Visibility.Hidden;
            spCreateUser.Visibility = Visibility.Hidden;

        }

        private void rbCreateUser_Checked(object sender, RoutedEventArgs e)
        {
            txtSelectUser.Visibility = Visibility.Hidden;
            cmbSelectUser.Visibility = Visibility.Hidden;
            btnLoginWindowLogin.Visibility = Visibility.Hidden;
            txtCreateUser.Visibility = Visibility.Visible;
            spCreateUser.Visibility = Visibility.Visible;
        }
    }

}
