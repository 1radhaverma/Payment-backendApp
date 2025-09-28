using PaymentApi.Models;

namespace PaymentApi.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponse> CreatePaymentAsync(PaymentRequest req);
        Task<TransactionRecord?> GetTransactionByPaymentIntentIdAsync(string paymentIntentId);

    }
}
