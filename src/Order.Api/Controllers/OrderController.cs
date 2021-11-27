using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using Order.Api.HttpClients;
using Order.Api.Models;
using System.Threading.Tasks;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPaymentClient _paymentClient;
        private readonly ITracer _tracer;

        public OrderController(IPaymentClient paymentClient, ITracer tracer)
        {
            _paymentClient = paymentClient;
            _tracer = tracer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(double paymentValue)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);

            var respose = await _paymentClient.DoPayment(new PaymentModel { Value = paymentValue });
            return Ok(respose.Value);
        }
    }
}
