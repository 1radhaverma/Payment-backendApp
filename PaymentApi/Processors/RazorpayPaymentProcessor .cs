using Microsoft.Extensions.Options;
using Razorpay.Api;
using PaymentApi.Config;
using PaymentApi.Interfaces;
using PaymentApi.Models;
using PaymentApi.Repositories;


namespace PaymentApi.Processors
{
    public class RazorpayPaymentProcessor : IPaymentProcessor
    {
        private readonly RazorpaySettings _cfg;
        private readonly ITransactionRepository _repo;
        public RazorpayPaymentProcessor(IOptions<RazorpaySettings> options)
        {
            _cfg = options.Value ?? throw new ArgumentNullException(nameof(options));
            if (string.IsNullOrWhiteSpace(_cfg.KeyId) || string.IsNullOrWhiteSpace(_cfg.KeySecret))
                throw new InvalidOperationException("Razorpay keys not configured.");
        }

        public Task<PaymentResponse> CreatePaymentIntentAsync(PaymentRequest req)
        {
            var client = new RazorpayClient(_cfg.KeyId, _cfg.KeySecret);

            var receiptId = $"rcpt_{DateTime.UtcNow.Ticks % 1000000}";
            // always < 40 chars, unique enough

            var options = new Dictionary<string, object>
    {
        { "amount", Convert.ToInt32(req.Amount * 100) }, // paise
        { "currency", string.IsNullOrWhiteSpace(req.Currency) ? "INR" : req.Currency.ToUpper() },
        { "receipt", receiptId },
        { "payment_capture", 1 } // auto capture
    };

            var order = client.Order.Create(options);

            var response = new PaymentResponse
            {
                PaymentIntentId = order["id"].ToString(),
                Status = order["status"].ToString(),
                Message = "Order created successfully",
                ClientSecret = null // Razorpay doesn't use this
            };

            return Task.FromResult(response);
        }

        public async Task<PaymentResponse> RetrievePaymentIntentAsync(string paymentIntentId)
        {
            // First fetch transaction from DB using Guid
            var tx = await _repo.GetByIdAsync(paymentIntentId);
            if (tx == null)
            {
                return new PaymentResponse
                {
                    PaymentIntentId = paymentIntentId.ToString(),
                    Status = "not_found",
                    Message = "Transaction not found in local DB",
                    ClientSecret = null
                };
            }

            // Now use Razorpay orderId (string) to fetch live status
            var client = new RazorpayClient(_cfg.KeyId, _cfg.KeySecret);
            try
            {
                var order = client.Order.Fetch(tx.PaymentIntentId);

                return new PaymentResponse
                {
                    PaymentIntentId = order["id"],
                    Status = order["status"].ToString(),
                    Message = "Order retrieved successfully",
                    ClientSecret = null
                };
            }
            catch (Exception ex)
            {
                return new PaymentResponse
                {
                    PaymentIntentId = tx.PaymentIntentId,
                    Status = "error",
                    Message = $"Error fetching from Razorpay: {ex.Message}",
                    ClientSecret = null
                };
            }
        }
    }
}
