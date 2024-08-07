using Application.Interfaces.StripeService;
using Stripe;
using Stripe.Checkout;

namespace Infrastructure.StripeService;

public class StripeService : IStripeService
{
    public async Task<string> GetStripePaymentUrl(decimal amount, string courseName, string paymentMethodType)
    {
        var options = new SessionCreateOptions
        {
            SuccessUrl = "https://localhost:7000",
            CancelUrl = "https://localhost:7000",
            LineItems = new List<SessionLineItemOptions>(),
            Mode = "payment"
        };

        var sessionLineItem = new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmount = (long)(amount * 100),
                Currency = "usd",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = courseName
                }
            },
            Quantity = 1
        };

        options.LineItems.Add(sessionLineItem);

        var service = new SessionService();
        Session session =  await service.CreateAsync(options);

        return session.Url;
    }
}
