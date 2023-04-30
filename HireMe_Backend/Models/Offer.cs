using HireMe_Backend.Models.DTOS;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace HireMe_Backend.Models
{
    public class Offer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }

        [Required]
        public string Title { get; init; }
        [Url]
        public string ImageUrl { get; init; }
        public string Description { get; init; }
        public float Prize { get; init; }

        //navigation properties
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ApplicationUser User { get; init; }
    }
}
