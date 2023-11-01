using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace YemekDünyasi.Models
{
    public class User
    {
        public string UserResimUrl { get; set; } // Resim parametresi
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string TelNo { get; set; }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        // Example usage
        public void ExampleUsage()
        {
            string userEnteredPassword = "myPassword123";
            string hashedPassword = HashPassword(userEnteredPassword);

            // Now you can store the hashedPassword in the database or wherever you need to keep it secure
        }




        [JsonIgnore]
        public ICollection<UserSepet>? UserSepets { get; set; }
        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
    }
}
