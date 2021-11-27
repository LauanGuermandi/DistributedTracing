using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using Payment.Api.Models;

namespace Payment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ITracer _tracer;

        public PaymentController(ITracer tracer)
        {
            _tracer = tracer;
        }

        [HttpPost]
        public ActionResult<double> Compute([FromBody] PaymentModel payment)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log($"Pagamento efetuado de R${payment.Value}.");
            return Ok(payment);
        }
    }
}
