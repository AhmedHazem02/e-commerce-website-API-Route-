using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.OrderAggragate;

namespace Talabat.Core.Specifications.OrderSpecs
{
    public class OrderSpec:BaseSpecification<Order>
    {
        public OrderSpec(string email):base(O=>O.BuyerEmail==email) {
            Include.Add(O => O.Items);
            Include.Add(O => O.DeliveryMethod);
            AddOrderByDesc(O => O.OrderDate);
        }

        public OrderSpec(string email,int Id) : base(O => O.BuyerEmail == email)
        {
            Include.Add(O => O.Items);
            Include.Add(O => O.DeliveryMethod);
            AddOrderByDesc(O => O.OrderDate);
        }
    }
}
