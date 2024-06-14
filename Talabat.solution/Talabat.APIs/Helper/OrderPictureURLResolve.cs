using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.OrderAggragate;

namespace Talabat.APIs.Helper
{
    public class OrderPictureURLResolve :IValueResolver<OrderIteml, OrderItemlDto, string>
    {
        private readonly IConfiguration _configuration;

    public OrderPictureURLResolve(IConfiguration configuration)
    {
        _configuration = configuration;
    }

        public string Resolve(OrderIteml source, OrderItemlDto destination, string destMember, ResolutionContext context)
        {
                     if (!string.IsNullOrEmpty(source.Product.PictureUrl))
            return $"{_configuration["BaseUrl"]}{source.Product.PictureUrl}";
        else return string.Empty;
        }
    }
}
