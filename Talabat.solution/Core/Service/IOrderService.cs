using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.OrderAggragate;

namespace Talabat.Core.Service
{
    public interface IOrderService
    {
        public Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DelveryMethodId, Address ShippingAddress);
        public Task<IReadOnlyList<Order>> GetAllOrderByEmailSpecUser(string BuyerEmail);
        public Task<Order> GetOrderByIdSpecUser(string buyerEmail,int Id);
    }
}
