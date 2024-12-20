using BancoXYZ.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace BancoXYZ.Views
{
    public partial class RegisterView : Window
    {
        private readonly RegisterViewModel _viewModel;

        public RegisterView()
        {
            InitializeComponent();
            _viewModel = new RegisterViewModel();
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            string currentAccount = CurrentAccountTextBox.Text;
            string name = NameTextBox.Text;
            string password = PasswordTextBox.Password;
            _viewModel.Register(currentAccount, name, password);
        }

        private void OnBackToLoginClick(object sender, RoutedEventArgs e)
        {
            MainView loginView = new MainView();
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
    }
}
