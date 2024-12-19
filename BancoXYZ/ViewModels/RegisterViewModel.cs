using BancoXYZ.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace BancoXYZ.ViewModels
{
    public class RegisterViewModel
    {
        private readonly UserService _userService;

        public RegisterViewModel()
        {
            _userService = new UserService();
        }

        public void Register(string currentAccount, string password)
        {
            if (string.IsNullOrEmpty(currentAccount) || string.IsNullOrEmpty(password))
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
                Password = _userService.HashPassword(password),
                Balance = 0
            };

            users.Add(newUser);
            SaveUsers(users);
            MessageBox.Show("Account created.");
        }

        private void SaveUsers(List<User> users)
        {
            string json = JsonSerializer.Serialize(users);
            File.WriteAllText(UserService.UsersFilePath, json);
        }
    }
}
