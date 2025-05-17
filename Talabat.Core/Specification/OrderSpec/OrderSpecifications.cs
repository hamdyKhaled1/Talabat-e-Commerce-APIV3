using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Core.Specification.OrderSpec
{
    public class OrderSpecifications:BaseSpecification<Order>
    {
        public OrderSpecifications(string email):base(O=>O.BuyerEmail==email)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            OrderByDesc = O => O.OrderDate;
        }

        public OrderSpecifications(int id,string email) : base(O => O.BuyerEmail == email && O.Id==id)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
    }
}
