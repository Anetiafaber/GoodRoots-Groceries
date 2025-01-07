using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group13_GoodRoots.Models
{
    public class ShopViewModel
    {
        public List<Product> Products { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> SortOptions { get; set; }
        public string SelectedCategoryName { get; set; }
        public string SelectedSortOption { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
