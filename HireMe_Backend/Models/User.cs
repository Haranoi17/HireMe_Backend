using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;


namespace HireMe_Backend.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }

        [Required]
        public string Name { get; init; }

        [Required]
        public string Email { get; init; }

        [Required]
        public string Password { get { return Password; } init { Password = hashPassword(value); } }

        public static string hashPassword(string password)
        {
            var sha = SHA256.Create();
            var passwordAsByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(passwordAsByteArray);
            return Convert.ToBase64String(hashedPassword);
        }
    }
}
