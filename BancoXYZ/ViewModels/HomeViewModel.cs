using BancoXYZ.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BancoXYZ.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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

        public void Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                _userService.UpdateUserBalance(User, Balance);
            }
        }

        public List<string> GetWithdrawOptions(decimal amount)
        {
            int[] denominations = { 100, 50, 20, 10 };
            var combinations = GenerateCombinations((int)amount, denominations);
            return combinations.Take(3).Select((combo, index) => $"{index + 1}) {combo}").ToList();
        }

        private List<string> GenerateCombinations(int amount, int[] denominations)
        {
            var result = new List<string>();
            var currentCombination = new List<int>();

            void AddCombination(int remainingAmount)
            {
                if (remainingAmount == 0)
                {
                    result.Add(FormatCombination(currentCombination));
                    return;
                }

                foreach (var denomination in denominations)
                {
                    if (remainingAmount >= denomination)
                    {
                        currentCombination.Add(denomination);
                        AddCombination(remainingAmount - denomination);
                        currentCombination.RemoveAt(currentCombination.Count - 1);
                    }
                }
            }

            AddCombination(amount);
            return result;
        }

        private string FormatCombination(List<int> combination)
        {
            var groupedDenominations = combination.GroupBy(d => d)
                                                  .Select(g => new { Denomination = g.Key, Count = g.Count() })
                                                  .OrderByDescending(x => x.Denomination);

            return string.Join(", ", groupedDenominations.Select(x => $"{x.Count} cédula{(x.Count > 1 ? "s" : "")} de {x.Denomination} reais"));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
