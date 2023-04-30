namespace HireMe_Backend.Models.DTOS
{
    public class OfferWithMinimalUserDto
    {
        public Offer Offer { get; init; }
        public MinimalUserDto User { get; init; }

        public OfferWithMinimalUserDto(Offer offer)
        {
            Offer = offer;
            User = new MinimalUserDto(offer.User);
        }
    };
}
