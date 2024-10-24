using Microsoft.Extensions.Configuration;
using OnlineStore.Core.Entities.Basket_Module;
using OnlineStore.Core.Entities.Order;
using OnlineStore.Core.Repositories.Contract.Basket;
using OnlineStore.Core.Services.Contract.Payment;
using OnlineStore.Core.UnitOfWork.Contract;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = OnlineStore.Core.Entities.Product;

namespace OnlineStore.Service.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basket;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentService(IBasketRepository basket, IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            _basket = basket;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntentIdAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            // Get Basket
            var basket = await _basket.GetBasketAsync(basketId);
            if (basket is null) return null;

            var service = new PaymentIntentService();
            var ShippingPrice = 0m;

            if(basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
                ShippingPrice = deliveryMethod.Cost;
            }

            if(basket.Items.Count>0)
            {
                foreach (var item in basket.Items) 
                {
                    var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(int.Parse(item.Id));
                    if (item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }
                }

            }

            var subTotal = basket.Items.Sum(P => P.Price * P.Quantity);

            PaymentIntent paymentIntent;
            if(string.IsNullOrEmpty(basket.PaymentInentId))
            {
                // create
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(subTotal + ShippingPrice)*100,
                    PaymentMethodTypes = new List<string> { "card"},
                    Currency = "usd"
                };
                paymentIntent = await service.CreateAsync(options);
                basket.PaymentInentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(subTotal + ShippingPrice) * 100,
                };
                paymentIntent = await service.UpdateAsync(basket.PaymentInentId,options);
                basket.PaymentInentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            
            basket = await _basket.UpdateBasketAsync(basket);
            if (basket is null) return null;
            return basket;
        }
    }
}
