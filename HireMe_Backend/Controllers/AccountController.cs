using HireMe_Backend.Models.DTOS;
using Microsoft.AspNetCore.Authentication;
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
                return Ok(Json("Registered successfully"));
            }

            return BadRequest(Json(result.Errors));
        }

        [HttpPost("checkCookie")]
        public async Task<IActionResult> CheckCookie()
        {
            var result = Request.Cookies.ContainsKey(".AspNetCore.Identity.Application");
            return Ok(Json(result));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var user = new IdentityUser() { UserName = loginUserDto.Name };

            var result = await signInManager.PasswordSignInAsync(user.UserName, loginUserDto.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return Ok(Json("User logged in"));
            }

            return BadRequest(Json("Wrong credentials"));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Ok(Json("Logged out"));
        }

        [HttpGet("user")]
        public async Task<IActionResult> getUser()
        {
            return Ok(Json(User.Identity.Name));
        }

    }
}
