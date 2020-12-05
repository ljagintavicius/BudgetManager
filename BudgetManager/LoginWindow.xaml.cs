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
        private readonly List<User> _usersList;
        public LoginWindow()
        {
            InitializeComponent();
            ICRUDRepository<User> userManager = new UserManager();
            _usersList = userManager.GetAll();
        }
    }
}
