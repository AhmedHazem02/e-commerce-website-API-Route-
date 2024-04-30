using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.OrderAggragate
{
    public class Order:BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderIteml> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail {  get; set; }
        public DateTimeOffset OrderDate {  get; set; }= DateTimeOffset.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress {  get; set; }
        public DeliveryMethod DeliveryMethod {  get; set; }

        public ICollection<OrderIteml> Items { get; set; }=new HashSet<OrderIteml>();

        public decimal SubTotal {  get; set; }

        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
        public string PaymentIntentId {  get; set; }= string.Empty;

    }
}
