using HireMe_Backend.Models;
using HireMe_Backend.Models.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace HireMe_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public UsersController(ApplicationDbContext db) { this.db = db; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(db.users);
        }

        public string HashUserPassword(string password) 
        {
            var sha = SHA256.Create();
            var passwordAsByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(passwordAsByteArray);
            return Convert.ToBase64String(hashedPassword);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RegisterUserDto registerDto)
        {

            var newUser = new User() { Id = Guid.NewGuid(), Name = registerDto.Name, Email = registerDto.Email, Password=HashUserPassword(registerDto.Password) };
            await db.users.AddAsync(newUser);
            await db.SaveChangesAsync();
            return RedirectToAction("Get");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var users = db.users.Where(user => user.Id == id).ToList();
            var exists = !users.IsNullOrEmpty();

            if (exists)
            {
                var user = users.First();
                db.users.Remove(user);
                await db.SaveChangesAsync();
                return Ok("user deleted");
            }

            return NotFound($"user with id: {id} not fount");
        }
    }
}
