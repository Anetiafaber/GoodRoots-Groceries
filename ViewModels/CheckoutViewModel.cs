using Group13_GoodRoots.Models;
using System.ComponentModel.DataAnnotations;

namespace Group13_GoodRoots.ViewModels
{
    public class CheckoutViewModel
    {
        public CustomerDetailsViewModel? CustomerDetails { get; set; }
        public PaymentDetailsViewModel? PaymentDetails { get; set; }

        public string Email { get; set; }  // Populated from IdentityUser


    }
}
