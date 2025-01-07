using System.Collections.Generic;

namespace Group13_GoodRoots.Models
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; } = 1; // Default quantity
        public List<Product> OtherProducts { get; set; }
    }
}
