using System.ComponentModel.DataAnnotations;

namespace Talabat.API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisPlayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumbre { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
