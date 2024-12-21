using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaymentService.Models;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private static readonly List<Payment> Payments = new();
        [HttpPost]
        public IActionResult ProcessPayment([FromBody] dynamic request)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(request.ToString());

            int orderId = (int)data["id"];

            decimal amount = (decimal)data["amount"];
            var payment = new Payment
            {
                OrderId = orderId,
                Amount = amount,
                Status = "Processed"
            };
            Payments.Add(payment);
            return Ok(payment);
        }
    }
}
