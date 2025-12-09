using System.ComponentModel.DataAnnotations;
using static Catalog.Domain.Constants;

namespace Catalog.Domain;

public class Promotion
{
    [Key]
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public PromoActiveStatus IsActive { get; private set; }
    public virtual ICollection<Game> Games { get; private set; } = new List<Game>();

    protected Promotion() { }

    public Promotion(string title, decimal discountPercentage, DateTime startDate, DateTime endDate)
    {
        ValidateDomain(title, discountPercentage, startDate, endDate);

        Id = Guid.NewGuid();
        Title = title;
        DiscountPercentage = discountPercentage;
        StartDate = startDate;
        EndDate = endDate;
        IsActive = PromoActiveStatus.Active;
    }

    public static void ValidateDomain(string title, decimal discountPercentage, DateTime startDate, DateTime endDate)
    {
        if (string.IsNullOrEmpty(title))
            throw new DomainException("Title is mandatory");

        if (discountPercentage <= 0 || discountPercentage > 100)
            throw new DomainException("Discount percentage must be between 0 and 100");

        if (startDate >= endDate)
            throw new DomainException("Start date must be before end date");
    }

}
