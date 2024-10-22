using OnlineStore.Core.Entities;
using OnlineStore.Core.Entities.Order;
using OnlineStore.Core.Repositories.Contract.Basket;
using OnlineStore.Core.Services.Contract.Orders;
using OnlineStore.Core.Specification.Orders;
using OnlineStore.Core.UnitOfWork.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public OrderService(IUnitOfWork unitOfWork,IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null) return null;

            var orderItems = new List<OrderItem>();

            if (basket.Items.Count > 0) 
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(int.Parse(item.Id));
                    var ProductOrderItem = new ProductItemOrder(product.Id.ToString(), product.Name,product.PictureUrl);
                    var orderItem = new OrderItem(ProductOrderItem, item.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }


            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(deliveryMethodId);

            var subTotal = orderItems.Sum(O => O.Price * O.Quantity);

            var order = new Order(buyerEmail,shippingAddress,deliveryMethod,orderItems,subTotal,"");

            await _unitOfWork.Repository<Order,int>().AddAsync(order);

            var result = await _unitOfWork.CompleteAsync();

            if(result <=0) return null;

            return order;
        }

        public async Task<Order?> GetOrderByIdFromSpecificUserAsync(string buyerEmail, int orderId)
        {
            var spec = new OrderSpecification(buyerEmail, orderId);
            var order = await _unitOfWork.Repository<Order,int>().GetByIdWithSpecAsync(spec);

            if (order is null) return null;

            return order;
        }

        public async Task<IEnumerable<Order>?> GetOrderFromSpecificUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);
            var orders = await _unitOfWork.Repository<Order, int>().GetAllWithSpecAsync(spec);

            if(orders  is null) return null;   

            return orders;
        }
    }
}
