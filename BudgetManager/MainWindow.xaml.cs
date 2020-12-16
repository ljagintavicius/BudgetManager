using BudgetManager.DL;
using System.Windows;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User SelectedUser { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            BudgetEntriesUserControl.Visibility = Visibility.Hidden;
            ShowSummaryUserControl.Visibility = Visibility.Hidden;
            MenuUserControl.btnBudgetEntries_ClickHandler += btnBudgetEntries_Click;
            MenuUserControl.btnBudgetEntries_ClickHandler += BudgetEntriesUserControl.ShowDataGrid;
            MenuUserControl.btnLogOut_ClickHandler += btnLogOut_Click;
            MenuUserControl.btnShowSummary_ClickHandler += btnShowSummary_Click;
        }

        private void btnBudgetEntries_Click(object sender, RoutedEventArgs e)
        {
            BudgetEntriesUserControl.SelectedUser = MenuUserControl.SelectedUser;
            BudgetEntriesUserControl.Visibility = Visibility.Visible;
            ShowSummaryUserControl.Visibility = Visibility.Hidden;
            ShowSummaryUserControl.HideTables();
        }
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            ShowSummaryUserControl.Visibility = Visibility.Hidden;
            BudgetEntriesUserControl.Visibility = Visibility.Hidden;
            ShowSummaryUserControl.HideTables();
        }

        private void btnShowSummary_Click(object sender, RoutedEventArgs e)
        {
            ShowSummaryUserControl.Visibility = Visibility.Visible;
            BudgetEntriesUserControl.Visibility = Visibility.Hidden;
        }

    }
}
