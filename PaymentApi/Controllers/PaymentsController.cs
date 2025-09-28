using Microsoft.AspNetCore.Mvc;
using PaymentApi.Models;
using PaymentApi.Services;

namespace PaymentApi.Controllers
{
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService) => _paymentService = paymentService;

        // POST api/payments/create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PaymentRequest req)
        {
            if (req.Amount <= 0) return BadRequest("Amount must be > 0");
            var result = await _paymentService.CreatePaymentAsync(req);
            return Ok(result);
        }

        // GET api/payments/transaction/{id}
        [HttpGet("transaction/razorpay/{paymentIntentId}")]
        public async Task<IActionResult> GetTransactionByPaymentIntent([FromRoute] string paymentIntentId)
        {
            var tx = await _paymentService.GetTransactionByPaymentIntentIdAsync(paymentIntentId);
            if (tx == null) return NotFound();
            return Ok(tx);
        }
    }
}