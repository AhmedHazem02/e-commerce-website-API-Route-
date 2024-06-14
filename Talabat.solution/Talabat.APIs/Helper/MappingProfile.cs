using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.OrderAggragate;
using AddressIdentity = Talabat.Core.Entities.Identity.Address;
using AddressAggragate = Talabat.Core.OrderAggragate.Address;


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

            CreateMap<AddressIdentity, AddressDto>().ReverseMap();
            CreateMap<AddressDto, AddressAggragate>();
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<Order, OrderDtoToReturn>()
                      .ForMember(O => O.DeliveryMethod, T => T.MapFrom(S => S.DeliveryMethod.ShortName))
                      .ForMember(O => O.DeliveryMethod, T => T.MapFrom(S => S.DeliveryMethod.Cost));

            CreateMap<OrderIteml, OrderItemlDto>()
                     .ForMember(M => M.ProductName, T => T.MapFrom(S => S.Product.ProductName))
                     .ForMember(M => M.ProductId, T => T.MapFrom(S => S.Product.ProductId))
                     .ForMember(M => M.PictureUrl, T => T.MapFrom(S => S.Product.PictureUrl));
        }
    }
}
