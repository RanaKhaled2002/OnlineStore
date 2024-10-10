using OnlineStore.Core.Entities.Basket_Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.DTOs.Basket
{
    public class CustomerBasketDTO
    {
        public string Id { get; set; }
        public List<BasketItem> items { get; set; }
    }
}
