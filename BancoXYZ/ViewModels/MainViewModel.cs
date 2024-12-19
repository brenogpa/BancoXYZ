using BancoXYZ.Models;
using BancoXYZ.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BancoXYZ.ViewModels
{
    public class MainViewModel
    {
        private readonly UserService _userService;

        public MainViewModel()
        {
            _userService = new UserService();
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
                    MessageBox.Show("Successful login!");
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

        public void Register()
        {
            RegisterView registerView = new RegisterView();
            registerView.Show();
        }
    }
}
