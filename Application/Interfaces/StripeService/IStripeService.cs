namespace Application.Interfaces.StripeService;

public interface IStripeService
{
    Task<string> GetStripePaymentUrl(decimal amount, string courseName, string paymentMethodType);
}
