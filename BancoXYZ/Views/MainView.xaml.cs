using BancoXYZ.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace BancoXYZ.Views
{
    public partial class MainView : Window
    {
        private readonly MainViewModel _viewModel;

        public MainView()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
        }

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            string currentAccount = CurrentAccountTextBox.Text;
            string password = PasswordTextBox.Password;
            _viewModel.Login(currentAccount, password);
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Register();
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
