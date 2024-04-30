using AutoMapper;
using System.Reflection;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helper
{
    public  class ProductPictureUrlResolve : IValueResolver<Product, ProductToReturn, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolve(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

        public string Resolve(Product source, ProductToReturn destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["BaseUrl"]}{source.PictureUrl}";
            else return string.Empty;
        }
    }
}
