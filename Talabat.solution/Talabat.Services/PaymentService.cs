using Microsoft.Extensions.Configuration;
using Stripe;
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
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration,
                              IBasketRepository basketRepository,
                              IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdataPayment(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];
            var Basket= await _basketRepository.GetBasketAsync(BasketId);
            if(Basket == null)return null;
            var ShippingPrice = 0M;
            if(Basket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod=await _unitOfWork.Repository<DeliveryMethod>().GetIdAsync(Basket.DeliveryMethodId.Value);
                ShippingPrice = DeliveryMethod.Cost;
            }
            if(Basket.Items.Count > 0)
            {
                foreach( var item in Basket.Items)
                {
                    var Product = await _unitOfWork.Repository<Product>().GetIdAsync(item.Id);
                    if(Product.Price!=item.Price)
                        item.Price=Product.Price;
                }

            }
                var SubTotal = Basket.Items.Sum(i => i.Quantity * i.Price);
            var Service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if(string.IsNullOrEmpty(Basket.PaymentIntentId))
            {
                // Create
                var Option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)SubTotal * 100 + (long)ShippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes=new List<string>() { "card"}
                };
                paymentIntent = await Service.CreateAsync(Option);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var Option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)SubTotal * 100 + (long)ShippingPrice * 100,
                };
                paymentIntent = await Service.UpdateAsync(Basket.PaymentIntentId,Option);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;

            }
            await _basketRepository.UpdataBasketAsync(Basket);
            return Basket;
        }
    }
}
