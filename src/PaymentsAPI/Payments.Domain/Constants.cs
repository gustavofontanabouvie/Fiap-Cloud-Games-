
namespace Payments.Domain;

public class Constants
{
    public enum OrderFulfillmentStatus
    {
        Pending = 0,
        Delivered = 1,
        Failed = 2,
    }

    public enum OrderPaymentStatus
    {
        Pending = 0,
        Paid = 1,
        Failed = 2,
        Refunded = 3
    }
}
