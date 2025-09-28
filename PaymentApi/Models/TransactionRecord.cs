namespace PaymentApi.Models
{
    public class TransactionRecord
    {
        public Guid Id { get; set; } 
        public string PaymentIntentId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "usd";
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? RawResponse { get; set; }  // for debugging / audit (avoid sensitive card data)
    }
}
