using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs;
using Talabat.API.Helper;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Core.Specification;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [CachedAttribute(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToRetuenDTO>>> GetProducts([FromQuery]ProductParameter parameter)
        {
            var spec = new ProductWithBrandAndTypeSpecification(parameter);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToRetuenDTO>>(products);
            var countspec=new ProductWithFilterationandcount(parameter);
            var count=await _unitOfWork.Repository<Product>().GetCountAsync(countspec);
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToRetuenDTO>> GetById(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecification(id);
            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);
            return Ok(_mapper.Map<Product,ProductToRetuenDTO> (product));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(types);
        }


    }
}
