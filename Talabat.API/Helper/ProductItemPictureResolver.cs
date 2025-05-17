using AutoMapper;
using AutoMapper.Execution;
using Talabat.API.DTOs;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.API.Helper
{
    public class OrderItemPictureResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;
        public OrderItemPictureResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.Product.PictureUrl}";
            return string.Empty;
        }
    }
}
