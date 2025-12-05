using System.ComponentModel.DataAnnotations;


namespace Users.Domain
{
    public class UserLibrary
    {
        [Key]
        public Guid Id { get; private set; }

        public DateTime AcquiredAt { get; private set; }

        public decimal PricePaid { get; private set; }

        public Guid UserId { get; private set; }
        public Guid GameId { get; private set; }

        public string GameTitle { get; private set; }

        public string CoverImageUrl { get; private set; }

        public virtual User User { get; private set; }

        protected UserLibrary() { }

        public UserLibrary(decimal pricePaid, Guid userId, Guid gameId, string gameTitle, string coverImageUrl)
        {
            ValidateDomain(pricePaid, gameTitle, coverImageUrl, gameId, userId);

            Id = Guid.NewGuid();
            AcquiredAt = DateTime.UtcNow;
            PricePaid = pricePaid;
            UserId = userId;
            GameId = gameId;
            GameTitle = gameTitle;
            CoverImageUrl = coverImageUrl;
        }

        private static void ValidateDomain(decimal pricePaid, string gameTitle, string coverImageUrl, Guid gameId, Guid userId)
        {
            if (pricePaid < 0)
                throw new DomainException("Price paid cannot be less than 0");

            if (string.IsNullOrEmpty(gameTitle))
                throw new DomainException("Game title is mandatory");

            if (string.IsNullOrEmpty(coverImageUrl))
                throw new DomainException("CoverImageUrl is mandatory");

            if (userId == Guid.Empty)
                throw new DomainException("User ID is invalid");

            if (gameId == Guid.Empty)
                throw new DomainException("Game ID is invalid");
        }
    }
}

