using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Group13_GoodRoots.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Please enter the order date!")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Please enter the total amount!")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Please enter the order status!")]
        public string OrderStatus { get; set; }

        [Required(ErrorMessage = "Please enter the user of the order!")]
        public string UserId { get; set; }

        // navigation property to UserDetails
        public UserDetails UserDetails { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
