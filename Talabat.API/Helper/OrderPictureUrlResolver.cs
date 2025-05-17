using AutoMapper;
using AutoMapper.Execution;
using Talabat.API.DTOs;
using Talabat.Core.Entites;

namespace Talabat.API.Helper
{
    public class OrderPictureUrlResolver : IValueResolver<Product, ProductToRetuenDTO, string>
    {
        private readonly IConfiguration _configuration;
        public OrderPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToRetuenDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";
            return string.Empty ;
        }
    }
}
