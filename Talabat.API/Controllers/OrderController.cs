using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.API.DTOs;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Service;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail=User.FindFirstValue(ClaimTypes.Email);
            var address=_mapper.Map<AddressDto,Address>(orderDto.shipToAddress);
            var order=await _orderService.CreateOrderAsync(buyerEmail,orderDto.BasketId,orderDto.DeliveryMethodId,address);
            if (order == null) return BadRequest();
            return Ok(_mapper.Map<Order,OrderToReturnDto>(order));

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders=await _orderService.GetOrdersForUserAsync(buyerEmail);
            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>>GetOrderForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order=await _orderService.GetOrdersByIdForUserAsync(id,buyerEmail);
            if (order is null) return NotFound();
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }

        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            var deliveryMethod=await _orderService.GetDeliveryMethodAsync();
            return Ok(deliveryMethod);
        }
    }
}
