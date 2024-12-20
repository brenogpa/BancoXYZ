using BancoXYZ.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BancoXYZ.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;
        private User _user;

        public User User
        {
            get => _user;
            private set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        public decimal Balance
        {
            get => _user.Balance;
            private set
            {
                if (_user.Balance != value)
                {
                    _user.Balance = value;
                    OnPropertyChanged();
                }
            }
        }

        public HomeViewModel(User user)
        {
            _userService = new UserService();
            User = user;
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                _userService.UpdateUserBalance(User, Balance);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
