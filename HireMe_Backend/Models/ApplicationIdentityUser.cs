using Microsoft.AspNetCore.Identity;

namespace HireMe_Backend.Models
{
    public class ApplicationUser : IdentityUser
    {

        //navigation properties
        public IEnumerable<Offer> offers { get; init; }
    }
}
