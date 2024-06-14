using Talabat.Core.OrderAggragate;

namespace Talabat.APIs.DTOs
{
    public class OrderDtoToReturn
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }

        public string Status { get; set; } 
        public Address ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost{ get; set; }

        public ICollection<OrderItemlDto> Items { get; set; } = new HashSet<OrderItemlDto>();

        public decimal SubTotal { get; set; }

        public decimal Total {  get; set; }
        public string PaymentIntentId { get; set; } 
    }
}
