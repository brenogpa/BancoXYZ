using BancoXYZ.Models;
using BancoXYZ.ViewModels;
using Xunit;

namespace Tests.ViewModels
{
    public class HomeViewModelTests
    {
        [Fact]
        public void Deposit_ShouldIncreaseBalance()
        {
            var user = new User { Balance = 100 };
            var viewModel = new HomeViewModel(user);

            viewModel.Deposit(50);

            Assert.Equal(150, viewModel.Balance);
        }

        [Fact]
        public void Withdraw_ShouldDecreaseBalance()
        {
            var user = new User { Balance = 100 };
            var viewModel = new HomeViewModel(user);

            viewModel.Withdraw(50);

            Assert.Equal(50, viewModel.Balance);
        }

        [Fact]
        public void Withdraw_ShouldNotAllowOverdraft()
        {
            var user = new User { Balance = 100 };
            var viewModel = new HomeViewModel(user);

            viewModel.Withdraw(150);

            Assert.Equal(100, viewModel.Balance);
        }

    }
}


