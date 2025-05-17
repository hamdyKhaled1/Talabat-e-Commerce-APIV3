using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Core.Specification.OrderSpec
{
    public class OrderWithPaymentIntentIdWithSpecififcation:BaseSpecification<Order>
    {
        public OrderWithPaymentIntentIdWithSpecififcation(string paymentIntentId):base(O=>O.PaymentIntentId==paymentIntentId)
        {
            
        }
    }
}
