using AutoMapper;
using Talabat.API.DTOs;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.API.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToRetuenDTO>()
                .ForMember(d=>d.ProductBrand,O=>O.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(d=>d.ProductType,O=>O.MapFrom(s=>s.ProductType.Name))
                .ForMember(d=>d.PictureUrl,O=>O.MapFrom<OrderPictureUrlResolver>());
            CreateMap<Talabat.Core.Entites.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<AddressDto,Talabat.Core.Entites.Order_Aggregate. Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(S => S.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(S => S.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(S => S.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(S => S.Product.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureResolver>());
        }
    }
}
