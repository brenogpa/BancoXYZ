using System.Text.Json;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace BancoXYZ.Models
{
    public class User
    {
        public string Account { get; set; }
        public string HashedPassword { get; set; }
    }

    public class UserService
    {
        public const string UsersFilePath = "users.json";

        public Dictionary<string, string> LoadUsers()
        {
            if (!File.Exists(UsersFilePath))
            {
                return new Dictionary<string, string>();
            }

            string json = File.ReadAllText(UsersFilePath);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
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
