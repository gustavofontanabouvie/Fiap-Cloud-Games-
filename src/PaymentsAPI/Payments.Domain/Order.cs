

using static Payments.Domain.Constants;

namespace Payments.Domain;

public class Order
{
    public Guid Id { get; private set; }
    public int Number { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public decimal PricePaid { get; private set; }

    public Guid UserId { get; private set; }

    public Guid GameId { get; private set; }

    public string? GatewayTransactionId { get; private set; }

    public OrderPaymentStatus PaymentStatus { get; private set; }

    public OrderFulfillmentStatus FulfillmentStatus { get; private set; }

    protected Order() { }

    public Order(int number, decimal pricePaid, Guid userId, Guid gameId)
    {
        ValidateDomain(pricePaid);

        Id = Guid.NewGuid();
        Number = number;
        PricePaid = pricePaid;
        UserId = userId;
        GameId = gameId;


        CreatedAt = DateTime.UtcNow;
        PaymentStatus = OrderPaymentStatus.Pending;
        FulfillmentStatus = OrderFulfillmentStatus.Pending;
        GatewayTransactionId = null;
    }

    public static void ValidateDomain(decimal price)
    {
        if (price < 0)
            throw new DomainException("Price cannot be negative");



    }
}
