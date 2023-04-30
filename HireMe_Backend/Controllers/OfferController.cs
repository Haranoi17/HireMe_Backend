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
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        public OfferController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = context;
            this.userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOffer( [FromBody] CreateOfferDto offerDto)
        {
            var currentUser = getCurrentlyLoggedInApplicationUser().Result;
            var offer = new Offer() { Id = Guid.NewGuid(), Description = offerDto.Description, ImageUrl = offerDto.ImageUrl, Prize = offerDto.Prize, Title = offerDto.Title, User = currentUser };

            var result = await dbContext.Offers.AddAsync(offer);
            await dbContext.SaveChangesAsync();

            return Ok(offer);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOffers()
        {
            try
            {
                var currentUser = getCurrentlyLoggedInApplicationUser().Result;
                
                var offers = currentUser.Offers.ToList();
                var result = offers.ConvertAll(offer=>new OfferWithMinimalUserDto(offer));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("user not logged in");
            }
        }

        [HttpGet("owner/{offerId}")]
        public async Task<IActionResult> GetOfferOwner([FromQuery] Guid offerId)
        {
            var offer = await dbContext.Offers.FindAsync(offerId);

            var result = new OfferWithMinimalUserDto(offer);
            return Ok(result);
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllOffers()
        {
            var offers = await dbContext.Offers.ToListAsync();
            var result = offers.ConvertAll(offer => new OfferWithMinimalUserDto(offer));
            return Ok(result);
        }

        private async Task<ApplicationUser> getCurrentlyLoggedInApplicationUser() 
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUser = await dbContext.Users.Where(user => user.Id == currentUserId).FirstAsync() as ApplicationUser;
            return currentUser;
        }
    }
}
