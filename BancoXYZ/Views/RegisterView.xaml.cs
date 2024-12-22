using BancoXYZ.Models;
using BancoXYZ.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace BancoXYZ.Views
{
    public partial class RegisterView : Window
    {
        private readonly UserService _userService;

        public RegisterView()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            string currentAccount = CurrentAccountTextBox.Text;
            string name = NameTextBox.Text;
            string password = PasswordTextBox.Password;
            Register(currentAccount, name, password);
        }

        private void OnBackToLoginClick(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();
        }

        private void CurrentAccountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private static bool IsTextNumeric(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }

        public void Register(string currentAccount, string name, string password)
        {
            if (string.IsNullOrEmpty(currentAccount) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            var users = _userService.LoadUsers();
            if (users.Exists(u => u.Account == currentAccount))
            {
                MessageBox.Show("Account already exists.");
                return;
            }

            var newUser = new User
            {
                Account = currentAccount,
                Name = name,
                Password = _userService.HashPassword(password),
                Balance = 0
            };

            users.Add(newUser);
            _userService.SaveUsers(users);
            MessageBox.Show("Account created.");
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();
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
