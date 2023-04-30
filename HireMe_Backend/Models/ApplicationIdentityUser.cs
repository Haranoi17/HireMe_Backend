using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HireMe_Backend.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Url]
        public string AvatarUrl { get; set; }

        //navigation properties
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Offer> Offers { get; init; }
    }
}
