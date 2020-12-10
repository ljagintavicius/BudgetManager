﻿using BudgetManager.BL;
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
        }



        private void btnNewUserCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewUser.Text))
            {
                _userManager.Add(new User { Name = txtNewUser.Text });
                MessageBox.Show($"User {txtNewUser.Text} created");
            }
            else
            {
                MessageBox.Show("Wrong input!");
            }

            txtNewUser.Text = string.Empty;
        }


        private void cmbUserList_DropDown(object sender, EventArgs e)
        {
            _usersList = _userManager.GetAll();
            cmbUserList.ItemsSource = _usersList.Select(z => z.Name);
        }

        private void btnLoginWindowLogin_Click(object sender, RoutedEventArgs e)
        {
            if (cmbUserList.SelectedIndex > -1)
            {
                SelectedUser = _userManager.SelectUserByName(cmbUserList.SelectedItem.ToString());
                btnLoginWindowLogin_ClickHandler(SelectedUser, e);
            }
            else
            {
                MessageBox.Show("User was not selected!");
            }
            cmbUserList.SelectedItem = null;
        }
    }

}
