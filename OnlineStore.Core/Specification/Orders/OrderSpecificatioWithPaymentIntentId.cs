using OnlineStore.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Specification.Orders
{
    public class OrderSpecificatioWithPaymentIntentId : BaseSpecification<Order,int>
    {
        public OrderSpecificatioWithPaymentIntentId(string PaymentIntentId) :
            base(O=>O.PaymentInentId == PaymentIntentId)
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
