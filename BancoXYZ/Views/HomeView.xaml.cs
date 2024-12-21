using System;
using System.Windows;
using BancoXYZ.Models;
using BancoXYZ.ViewModels;

namespace BancoXYZ.Views
{
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
            LoginView mainView = new LoginView();
            mainView.Show();
            this.Close();
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

        private void OnWithdrawClick(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(WithdrawAmountTextBox.Text, out decimal withdrawAmount))
            {
                if (withdrawAmount > _viewModel.Balance)
                {
                    MessageBox.Show("Insufficient balance.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var options = _viewModel.GetWithdrawOptions(withdrawAmount);
                WithdrawOptionsListBox.ItemsSource = options;
                if (options.Count > 0)
                {
                    ConfirmWithdrawButton.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("No valid withdrawal options available.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid amount.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnConfirmWithdrawClick(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(WithdrawAmountTextBox.Text, out decimal withdrawAmount))
            {
                _viewModel.Withdraw(withdrawAmount);
                MessageBox.Show("Withdrawal successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                WithdrawAmountTextBox.Clear();
                WithdrawOptionsListBox.ItemsSource = null;
                ConfirmWithdrawButton.Visibility = Visibility.Collapsed;
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

