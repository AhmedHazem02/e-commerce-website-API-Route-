using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturn>()
                      .ForMember(D => D.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                      .ForMember(D => D.ProductType, O => O.MapFrom(S => S.ProductType.Name))
                      .ForMember(D => D.PictureUrl, O => O.MapFrom<ProductPictureUrlResolve>());

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
           
        }
    }
}
