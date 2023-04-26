namespace HireMe_Backend.Models.DTOS
{
    public class CreateOfferDto
    {
        public string Title { get; init; }
        public string ImageUrl { get; init; }
        public string Description { get; init; }
        public float Prize { get; init; }
    }
}
