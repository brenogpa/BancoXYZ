The project is developed using C# and WPF, targeting .NET 8.
The system allows users to perform deposits, withdrawals, and view possible combinations of banknotes for withdrawal amounts.

Features
•	User Authentication: Basic user model and authentication logic.
•	Deposit and Withdrawal: Users can deposit and withdraw money, with balance updates reflected in real-time.
•	Banknote Combinations: Logic to generate up to three possible combinations of banknotes for a given withdrawal amount.
•	User Interface: Clean and user-friendly interface for interacting with the ATM system.

Project Structure
•	BancoXYZ.Models: Contains the User model.
•	BancoXYZ.ViewModels: Contains the HomeViewModel which handles the business logic.
•	BancoXYZ.Views: Contains the WPF views (HomeView, LoginView, RegisterView) for user interaction.
•	Tests: Contains unit tests for the HomeViewModel.
