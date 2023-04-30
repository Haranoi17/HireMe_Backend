namespace HireMe_Backend.Models.DTOS
{
    public class MinimalUserDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string AvatarUrl { get; init; }

        public MinimalUserDto() { }

        public MinimalUserDto(ApplicationUser user) {
            Id = user.Id;
            Name = user.UserName;
            AvatarUrl = user.AvatarUrl;
        }
    }
}
