namespace PaymentApi.Models
{
    public class PaymentRequest
    {
        public decimal Amount { get; set; }                // decimal in major units (e.g., 12.50)
        public string Currency { get; set; } = "usd";
        public string? Description { get; set; }
        public string? ReceiptEmail { get; set; }
        public string? PaymentMethodId { get; set; }
    }
}
