using Group13_GoodRoots.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Group13_GoodRoots.Models;
using Group13_GoodRoots.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Group13_GoodRoots.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext _ctx { get; set; }

        public ShopController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Index(string selectedCategoryName = "All", string selectedSortOption = "priceLowHigh", int page = 1)
        {

            Console.WriteLine("Selected Category: " + selectedCategoryName);
            Console.WriteLine("Selected Sort Option: " + selectedSortOption);
            Console.WriteLine("Current Page: " + page);

            var categories = _ctx.Categories.ToList();

            // Populate categories dropdown
            var categoryList = new List<SelectListItem>
            {
                new SelectListItem { Text = "All Categories", Value = "All", Selected = selectedCategoryName == "All" }
            };

            categoryList.AddRange(categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryName,
                Selected = c.CategoryName == selectedCategoryName
            }));

            // Populate sort options dropdown
            var sortOptions = new List<SelectListItem>
            {
                new SelectListItem { Text = "Price: Low to High", Value = "priceLowHigh", Selected = selectedSortOption == "priceLowHigh" },
                new SelectListItem { Text = "Price: High to Low", Value = "priceHighLow", Selected = selectedSortOption == "priceHighLow" },
                new SelectListItem { Text = "Alphabetical: A-Z", Value = "nameAZ", Selected = selectedSortOption == "nameAZ" },
                new SelectListItem { Text = "Alphabetical: Z-A", Value = "nameZA", Selected = selectedSortOption == "nameZA" }
            };

            // Filter products based on the category name
            var products = selectedCategoryName == "All"
                ? _ctx.Products
                : _ctx.Products.Where(p => p.Category.CategoryName == selectedCategoryName);

            // Apply sorting
            products = selectedSortOption switch
            {
                "priceLowHigh" => products.OrderBy(p => p.IsSale && p.SalePrice.HasValue ? p.SalePrice : p.Price),
                "priceHighLow" => products.OrderByDescending(p => p.IsSale && p.SalePrice.HasValue ? p.SalePrice : p.Price),
                "nameAZ" => products.OrderBy(p => p.ProductName),
                "nameZA" => products.OrderByDescending(p => p.ProductName),
                _ => products
            };

            // Pagination
            int pageSize = 9;

            // Get the total number of products
            var totalProducts = products.Count();

            // Calculate the total number of pages
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            // Fetch the products for the current page
            var productsPage = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Create the ViewModel
            var viewModel = new ShopViewModel
            {
                Products = productsPage,
                Categories = categoryList,
                SortOptions = sortOptions,
                SelectedCategoryName = selectedCategoryName,
                SelectedSortOption = selectedSortOption,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(ShopViewModel viewModel)
        {
            /*var selectedCategoryId = viewModel.SelectedCategoryId;

            var categories = _ctx.Categories.ToList();*/

            /*var categoryList = new List<SelectListItem>
            {
                new SelectListItem { Text = "All Categories", Value = "", Selected = selectedCategoryId == 0 }
            };

            categoryList.AddRange(categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString(),
                Selected = c.CategoryId == selectedCategoryId
            }));*/

            // Filter products based on the selected category
            /*var products = _ctx.Products
                .Where(p => (selectedCategoryId == 0 || p.CategoryId == selectedCategoryId))
                .ToList(); // Show all products if no category is selected*/

            return RedirectToAction("Index", new
            {
                selectedCategoryName = viewModel.SelectedCategoryName,
                selectedSortOption = viewModel.SelectedSortOption,
                page = (viewModel.CurrentPage < 1) ? 1 : viewModel.CurrentPage
            });

            // Create the ViewModel with the filtered products and the selected category
            /*var viewModel = new ShopViewModel
            {
                Products = products,
                Categories = categoryList,
                SelectedCategoryId = selectedCategoryId // Store the selected category
            };*/

            /*return View("Index", viewModel);*/
        }
    }
}
