using System.ComponentModel.DataAnnotations;

namespace Talabat.API.DTOs
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto shipToAddress { get; set; }
    }
}
