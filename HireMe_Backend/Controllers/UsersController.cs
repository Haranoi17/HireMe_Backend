using HireMe_Backend.Controllers.UserValidators;
using HireMe_Backend.Models;
using HireMe_Backend.Models.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HireMe_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly RegisterUserValidator userValidator;

        public UsersController(ApplicationDbContext dbContext) { 
            this.dbContext = dbContext; 
            this.userValidator = new RegisterUserValidator(dbContext); 
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(dbContext.users);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RegisterUserDto registerDto)
        {
            if (!userValidator.isUserValid(registerDto)) 
            {
                return BadRequest(userValidator.getMessages(registerDto));
            }

            var newUser = new User() { Id = Guid.NewGuid(), Name = registerDto.Name, Email = registerDto.Email, Password = registerDto.Password };
            await dbContext.users.AddAsync(newUser);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Get");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var users = dbContext.users.Where(user => user.Id == id).ToList();
            var exists = !users.IsNullOrEmpty();

            if (exists)
            {
                var user = users.First();
                dbContext.users.Remove(user);
                await dbContext.SaveChangesAsync();
                return Ok("user deleted");
            }

            return NotFound($"user with id: {id} not fount");
        }
    }
}
