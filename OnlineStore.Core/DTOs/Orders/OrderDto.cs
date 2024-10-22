using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.DTOs.Orders
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public string DeliveryMethodId { get; set; }
        public AddressDto shippingAddress { get; set; }
}
}
