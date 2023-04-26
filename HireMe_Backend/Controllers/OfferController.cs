using HireMe_Backend.Models;
using HireMe_Backend.Models.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HireMe_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public OfferController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffer(CreateOfferDto offerDto)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var offer = new Offer() { Id = Guid.NewGuid(), Description = offerDto.Description, ImageUrl = offerDto.ImageUrl, Prize = offerDto.Prize, Title = offerDto.Title, UserId = Guid.Parse(currentUserId) };

            var result = await context.Offers.AddAsync(offer);
            await context.SaveChangesAsync();

            return Ok(offer);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOffers()
        {
            try
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = await context.Offers.Where(offer => offer.UserId == Guid.Parse(currentUserId)).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("user not logged in");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllOffers()
        {
            var result = await context.Offers.ToListAsync();
            return Ok(result);
        }
    }
}
