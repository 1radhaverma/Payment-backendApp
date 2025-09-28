using PaymentApi.Models;

namespace PaymentApi.Interfaces
{
    public interface IPaymentProcessor
    {
        Task<PaymentResponse> CreatePaymentIntentAsync(PaymentRequest req);
        Task<PaymentResponse> RetrievePaymentIntentAsync(string paymentIntentId);
    }
}
