using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Service;
using Talabat.Core.Specification.OrderSpec;

namespace Talabat.Service
{
    
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
       
        public OrderService(IBasketRepository basketRepository,IUnitOfWork unitOfWork ,IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService= paymentService;
            
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            var orderItems=new List<OrderItem>();
            if(basket?.Items?.Count > 0)
            {
                foreach(var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>() .GetByIdAsync(item.Id);
                    var ProductItemOrder = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(ProductItemOrder, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            var deliveryMethod=await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            var spec = new OrderWithPaymentIntentIdWithSpecififcation(basket.PaymentIntentId);
            var exisitingorder=await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            if(exisitingorder is not null)
            {
                _unitOfWork.Repository<Order>().Delete(exisitingorder);
                await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);
            }
            var order=new Order(buyerEmail,shippingAddress,deliveryMethod,orderItems,subtotal,basket.PaymentIntentId);
            await _unitOfWork.Repository<Order>().Add(order);
           var result= await _unitOfWork.Complete();
            if (result <= 0) return null;
            return order;

        }
        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec=new OrderSpecifications(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }
        public async Task<Order> GetOrdersByIdForUserAsync(int orderId, string buyerEmail)
        {
            var spec=new OrderSpecifications(orderId,buyerEmail);
            var orders=await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            return orders;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            var deliveryMethod=await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethod;
        }
    }
}
