namespace PaymentApi.Models
{
    public class PaymentResponse
    {
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }          // returned when frontend confirms
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
