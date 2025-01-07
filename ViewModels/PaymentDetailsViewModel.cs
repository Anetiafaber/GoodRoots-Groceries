using System.ComponentModel.DataAnnotations;
using Group13_GoodRoots.Validators;

namespace Group13_GoodRoots.ViewModels
{
    public class PaymentDetailsViewModel
    {
        [Required]
        public string CardName { get; set; }

        [Required]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must be 16 digits.")]
        public string CardNumber { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Server:Expiry date must be in MM/yy format.")]
        [FutureDate]      
        public string ExpiryDate { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV must be 3 digits.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVV must be 3 digits.")]
        public string CVV { get; set; }

    }
}
