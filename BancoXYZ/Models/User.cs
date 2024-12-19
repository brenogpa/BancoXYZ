using System.Text.Json;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace BancoXYZ.Models
{
    public class User
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
    }

    public class UserService
    {
        public const string UsersFilePath = "users.json";

        public List<User> LoadUsers()
        {
            if (!File.Exists(UsersFilePath))
            {
                return new List<User>();
            }

            string json = File.ReadAllText(UsersFilePath);
            return JsonSerializer.Deserialize<List<User>>(json);
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public bool VerifyPassword(string password, string storedHashedPassword)
        {
            string hashedPassword = HashPassword(password);
            return hashedPassword == storedHashedPassword;
        }
    }
}
