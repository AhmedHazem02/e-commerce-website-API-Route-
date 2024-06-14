using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.OrderAggragate;
using Talabat.Core.Repositories;
using Talabat.Core.Service;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.OrderSpecs;

namespace Talabat.Service
{
    public class OrderServices : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderServices(IBasketRepository basketRepo,IUnitOfWork unitOfWork) {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DelveryMethodId, Address ShippingAddress)
        {

            var Basket = await _basketRepo.GetBasketAsync(BasketId);
            // Items
            var OrderItems=new List<OrderIteml>();
            if(Basket?.Items.Count > 0)
            {
                foreach(var item in Basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetIdAsync(item.Id);
                    var OrderItemProduct = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                    var OrderItem=new OrderIteml(OrderItemProduct,product.Price,item.Quantity);
                    OrderItems.Add(OrderItem);

                }   
            }

            // SubTotal
            var SubTotal =  OrderItems.Sum(Items => (Items.Price * Items.Quantity));

            //Get Deilvery Method

            var DelveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetIdAsync(DelveryMethodId);

            //Create Order
            var Order=new Order(BuyerEmail,ShippingAddress, DelveryMethod,OrderItems,SubTotal,Basket.PaymentIntentId);

            // Save in Database
            await _unitOfWork.Repository<Order>().Add(Order);

            // SaveChange
            var res=await _unitOfWork.CompleteAsync();
            if (res <= 0) return null;

            return Order;
        }

        public async Task<IReadOnlyList<Order>> GetAllOrderByEmailSpecUser(string BuyerEmail)
        {
            var Spec = new OrderSpec(BuyerEmail);
            var Order =await _unitOfWork.Repository<Order>().GetAllBySpec(Spec);
            return Order;
        }

        public async Task<Order> GetOrderByIdSpecUser(string buyerEmail, int Id)
        {
             var Spec=new OrderSpec(buyerEmail, Id);
            var Order = await _unitOfWork.Repository<Order>().GetByIDSpec(Spec);
            return Order;
        }
    }
}
