using PaymentApi.Interfaces;
using PaymentApi.Models;
using PaymentApi.Repositories;

namespace PaymentApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentProcessor _processor;
        private readonly ITransactionRepository _repo;
        public PaymentService(IPaymentProcessor processor, ITransactionRepository repo)
        {
            _processor = processor;
            _repo = repo;
        }

        public async Task<PaymentResponse> CreatePaymentAsync(PaymentRequest req)
        {
            var resp = await _processor.CreatePaymentIntentAsync(req);

            var tx = new TransactionRecord
            {
                PaymentIntentId = resp.PaymentIntentId ?? string.Empty,
                Amount = req.Amount,
                Currency = req.Currency,
                Status = resp.Status ?? "unknown",
                RawResponse = $"client_secret:{resp.ClientSecret}"
            };

            await _repo.SaveAsync(tx);
            return resp;
        }

        public Task<TransactionRecord?> GetTransactionByPaymentIntentIdAsync(string paymentIntentId)
        {
            return _repo.GetByPaymentIntentIdAsync(paymentIntentId);
        }
    }
}