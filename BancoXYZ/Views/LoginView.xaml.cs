using BancoXYZ.Models;
using BancoXYZ.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace BancoXYZ.Views
{
    public partial class LoginView : Window
    {
        private readonly UserService _userService;

        public LoginView()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            string currentAccount = CurrentAccountTextBox.Text;
            string password = PasswordTextBox.Password;
            Login(currentAccount, password);
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            RegisterView registerView = new RegisterView();
            registerView.Show();
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

        public void Login(string currentAccount, string password)
        {
            if (string.IsNullOrEmpty(currentAccount) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            var users = _userService.LoadUsers();
            var user = users.FirstOrDefault(u => u.Account == currentAccount);

            if (user != null)
            {
                if (_userService.VerifyPassword(password, user.Password))
                {
                    HomeView homeView = new HomeView(user);
                    homeView.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Current account not found.");
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
