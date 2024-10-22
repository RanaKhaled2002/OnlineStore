using OnlineStore.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Specification.Orders
{
    public class OrderSpecification : BaseSpecification<Order,int>
    {
        public OrderSpecification(string buyerEmail,int orderId) 
            : base(O=>O.BuyerEmail ==  buyerEmail && O.Id == orderId)
        {
            ApplyIncludes();
        }

        public OrderSpecification(string buyerEmail)
            : base(O => O.BuyerEmail == buyerEmail)
        {
            ApplyIncludes();
        }

        private void ApplyIncludes()
        {
            Includes.Add(P => P.DeliveryMethod);
            Includes.Add(P => P.Items);
        }
    }
}
