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
using System.Windows.Shapes;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private List<User> _usersList;
        private readonly ICRUDRepository<User> _userManager;
        
        public LoginWindow()
        {
            InitializeComponent();
            _userManager = new UserManager();
            //_usersList = _userManager.GetAll();
            //cmbUserList.ItemsSource = _usersList.Select(z => z.Name);
        }



        private void btnNewUserCreate_Click(object sender, RoutedEventArgs e)
        {
            _userManager.Add(new User { Name = txtNewUser.Text });
            MessageBox.Show($"User {txtNewUser.Text} created");
            txtNewUser.Text = string.Empty;
        }


        private void cmbUserList_DropDown(object sender, EventArgs e)
        {
            _usersList = _userManager.GetAll();
            cmbUserList.ItemsSource = _usersList.Select(z => z.Name);
        }

        //private void txtNewUser_IsKeyboardFocusChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    txtNewUser.Text = string.Empty;
        //}
    }

}
