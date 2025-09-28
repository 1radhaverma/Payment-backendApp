using PaymentApi.Models;

namespace PaymentApi.Repositories
{
    public interface ITransactionRepository
    {
        Task SaveAsync(TransactionRecord tx);
        Task<TransactionRecord?> GetByIdAsync(string id);
        Task<TransactionRecord?> GetByPaymentIntentIdAsync(string paymentIntentId);
    }

}
