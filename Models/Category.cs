using System.ComponentModel.DataAnnotations;

namespace Group13_GoodRoots.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter the category name!")]
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
