using HireMe_Backend.Models;
using HireMe_Backend.Models.DTOS;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HireMe_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = new ApplicationUser() { UserName = registerUserDto.Name, Email = registerUserDto.Email };

            var result = await userManager.CreateAsync(user, registerUserDto.Password);

            if (result.Succeeded)
            {
                List<Claim> claims = new List<Claim>(){
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };
                await signInManager.SignInWithClaimsAsync(user, isPersistent: false, claims);
                return Ok(Json("Registered successfully"));
            }

            return BadRequest(Json(result.Errors));
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var user = await userManager.FindByNameAsync(loginUserDto.Name);

            if (user == null)
            {
                return BadRequest(Json("Wrong credentials"));
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, loginUserDto.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                List<Claim> claims = new List<Claim>(){
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                await signInManager.SignInWithClaimsAsync(user, isPersistent: false, claims);
                return Ok(Json("User logged in"));
            }

            return BadRequest(Json("Something wrong"));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Ok(Json("Logged out"));
        }

        [HttpPost("isLoggedIn")]
        public async Task<IActionResult> CheckIfLoggedIn()
        {
            return Ok(Json(User.Identity.IsAuthenticated));
        }

        [HttpGet("user")]
        public async Task<IActionResult> getUser()
        {
            return Ok(Json(User.Identity.Name));
        }

    }
}
