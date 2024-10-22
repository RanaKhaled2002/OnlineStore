using OnlineStore.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Services.Contract.Orders
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail,string basketId,int deliveryMethodId,Address shippingAddress);
        Task<IEnumerable<Order>?> GetOrderFromSpecificUserAsync(string buyerEmail);
        Task<Order?> GetOrderByIdFromSpecificUserAsync(string buyerEmail, int orderId);
    }
}
