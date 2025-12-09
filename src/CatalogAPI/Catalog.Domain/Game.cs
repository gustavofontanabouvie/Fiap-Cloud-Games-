using System.ComponentModel.DataAnnotations;
using static Catalog.Domain.Constants;

namespace Catalog.Domain;

public class Game
{
    [Key]
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public string Developer { get; private set; }
    public decimal Price { get; private set; }
    public string CoverImageUrl { get; private set; }
    public GameAvailabilityStatus IsAvailable { get; private set; }
    public virtual ICollection<Promotion> Promotions { get; private set; } = new List<Promotion>();

    protected Game() { }

    public Game(string title, string description, DateTime releaseDate, string developer, decimal price, string coverImageUrl)
    {
        ValidateDomain(title, description, developer, price, coverImageUrl);

        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        ReleaseDate = releaseDate;
        Developer = developer;
        Price = price;
        CoverImageUrl = coverImageUrl;
        IsAvailable = GameAvailabilityStatus.Available;
    }

    public void UpdateAvailability(GameAvailabilityStatus availabilityStatus)
    {
        IsAvailable = availabilityStatus;
    }

    public void AddPromotion(Promotion promotion)
    {
        bool hasOverLap = Promotions.Any(p => p.StartDate < promotion.EndDate && p.EndDate > promotion.StartDate);

        if (hasOverLap)
            throw new DomainException("conflicts with an existing promotion dates");

        Promotions.Add(promotion);
    }

    public static void ValidateDomain(string title, string description, string developer, decimal price, string coverImageUrl)
    {
        if (string.IsNullOrEmpty(title))
            throw new DomainException("Title is mandatory");

        if (string.IsNullOrEmpty(description))
            throw new DomainException("Description is mandatory");

        if (string.IsNullOrEmpty(developer))
            throw new DomainException("Developer is mandatory");

        if (price < 0)
            throw new DomainException("Price cannot be negative");

        if (string.IsNullOrEmpty(coverImageUrl))
            throw new DomainException("Cover image URL is mandatory");
    }
}
