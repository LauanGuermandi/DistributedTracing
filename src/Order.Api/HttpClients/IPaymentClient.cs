using Order.Api.Models;
using Refit;
using System.Threading.Tasks;

namespace Order.Api.HttpClients
{
    public interface IPaymentClient
    {
        [Post("/api/payment")]
        Task<PaymentModel> DoPayment([Body] PaymentModel payment);
    }
}
