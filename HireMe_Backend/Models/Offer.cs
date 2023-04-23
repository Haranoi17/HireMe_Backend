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
        public string ImageUrl { get; init; }
        public string Description { get; init; }
        public float prize { get; init; }
    }
}
