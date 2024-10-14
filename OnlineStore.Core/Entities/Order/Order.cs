using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Entities.Order
{
    public class Order : BaseEntity<int>
    {
        public string BuyerEmail { get; set; }

        public DateTimeOffset orderDate { get; set; } = DateTimeOffset.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; }

        public int DeliveryMethodId { get; set; } //FK

        public DeliveryMethod DeliveryMethod { get; set; }

        public ICollection<OrderItem> Items { get; set; }

        public decimal SubTotal { get; set; }

        public decimal GetTotal => SubTotal + DeliveryMethod.Cost;

        public string PaymentInentId { get; set; }
    }
}
