using BancoXYZ.Models;
using BancoXYZ.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BancoXYZ.ViewModels
{
    public class LoginViewModel
    {
        private readonly UserService _userService;
        private readonly Window _loginView;

        public LoginViewModel(Window loginView)
        {
            _userService = new UserService();
            _loginView = loginView;
        }


    }
}
