using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Group13_GoodRoots.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter the product name!")]
        public string ProductName { get; set; }

        public string ShortDescription { get; set; }

        public string? LongDescription { get; set; }

        [Required(ErrorMessage = "Please enter the price of the product!")]
        [Range(0.001, 1000, ErrorMessage = "The price should be between 0 and 1000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter the stock quantity!")]
        [Range(0, 1000, ErrorMessage = "The stock quantity should be between 0 and 1000.")]
        public int StockQuantity { get; set; }
        public string? Image { get; set; }

        [Required(ErrorMessage = "Please specify if the product is on sale!")]
        public bool IsSale { get; set; }
        public decimal? SalePrice { get; set; }

        [Required(ErrorMessage = "Please enter the category of the product!")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
