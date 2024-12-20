using System;
using System.Windows;
using BancoXYZ.Models;
using BancoXYZ.ViewModels;

namespace BancoXYZ.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : Window
    {
        private HomeViewModel _viewModel;

        public HomeView(User user)
        {
            InitializeComponent();
            _viewModel = new HomeViewModel(user);
            DataContext = _viewModel;
        }

        private void OnLogoutClick(object sender, RoutedEventArgs e)
        {
            MainView mainView = new MainView();
            mainView.Show();
        }

        private void OnDepositClick(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(DepositAmountTextBox.Text, out decimal depositAmount))
            {
                _viewModel.Deposit(depositAmount);
                DepositAmountTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a valid amount.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
