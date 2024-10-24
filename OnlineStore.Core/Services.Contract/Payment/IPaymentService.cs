using OnlineStore.Core.Entities.Basket_Module;
using OnlineStore.Core.Entities.Order;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Services.Contract.Payment
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntentIdAsync(string basketId);
        Task<Order> UpdatePaymentIntentForSuccessedOrFailed(string PaymentIntentId,bool flag);
    }
}
