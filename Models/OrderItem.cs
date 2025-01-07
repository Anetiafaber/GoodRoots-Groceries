using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Group13_GoodRoots.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        [Required(ErrorMessage = "Please enter the quantity!")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please enter the unit price!")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Please enter the order id!")]
        public int OrderId { get; set; }

        // navigation property to Order model
        public Order Order { get; set; }

        [Required(ErrorMessage = "Please enter the product id!")]
        public int ProductId { get; set; }

        // navigation property to Product model
        public Product Product { get; set; }
    }
}
