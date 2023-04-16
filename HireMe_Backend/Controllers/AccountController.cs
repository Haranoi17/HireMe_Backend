using HireMe_Backend.Models.DTOS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HireMe_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = new IdentityUser() { UserName = registerUserDto.Name, Email = registerUserDto.Email };

            var result = await userManager.CreateAsync(user, registerUserDto.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return Ok("User registered");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var user = new IdentityUser() { UserName = loginUserDto.Name };

            var result = await signInManager.PasswordSignInAsync(user.UserName, loginUserDto.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return Ok("User logged in");
            }

            return BadRequest("Wrong credentials");
        }
    }
}
