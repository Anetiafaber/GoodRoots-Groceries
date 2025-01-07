using Group13_GoodRoots.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Group13_GoodRoots.Models;
using Group13_GoodRoots.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Group13_GoodRoots.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _ctx { get; set; }

        public ProductController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        // GET: Product Details
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Product ID");
            }

            // Fetch the current product
            var product = _ctx.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            // Fetch random products excluding the current product
            var otherProducts = _ctx.Products
                .Where(p => p.ProductId != id)
                .Take(4)
                .ToList();

            // Create ViewModel
            var viewModel = new ProductDetailsViewModel
            {
                Product = product,
                Quantity = 1, // Default quantity
                OtherProducts = otherProducts
            };

            // Check for TempData message
            if (TempData.ContainsKey("CartMessage"))
            {
                ViewBag.CartMessage = TempData["CartMessage"];
            }

            return View(viewModel);
        }

        // POST: Handle Increment/Decrement
        [HttpPost]
        public IActionResult Details(int id, string action, int quantity)
        {
            if (id <= 0 || quantity <= 0)
            {
                return BadRequest("Invalid Product Data");
            }

            // Fetch the current product
            var product = _ctx.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            // Adjust quantity based on the action
            if (action == "increment" && quantity < product.StockQuantity)
            {
                quantity++;
            }
            else if (action == "decrement" && quantity > 1)
            {
                quantity--;
            }

            // Fetch random products excluding the current product
            var otherProducts = _ctx.Products
                .Where(p => p.ProductId != id)
                .Take(4)
                .ToList();

            // Create ViewModel
            var viewModel = new ProductDetailsViewModel
            {
                Product = product,
                Quantity = quantity,
                OtherProducts = otherProducts
            };

            return View(viewModel);
        }

        // POST: Add to Cart
        [HttpPost]
        public IActionResult AddToCart(int id, int quantity)
        {
            if (id <= 0 || quantity <= 0)
            {
                TempData["CartMessage"] = "Invalid product or quantity.";
                return RedirectToAction("Details", new { id });
            }

            // Fetch the product
            var product = _ctx.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                TempData["CartMessage"] = "Product not found.";
                return RedirectToAction("Details", new { id });
            }

            // Fetch cart or initialize a new one
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Check if the item already exists in the cart
            var existingCartItem = cart.FirstOrDefault(c => c.Product.ProductId == id);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
                TempData["CartMessage"] = $"{quantity} more item(s) added to the cart. Current Quantity: {existingCartItem.Quantity}";
            }
            else
            {
                cart.Add(new CartItem { Product = product, Quantity = quantity });
                TempData["CartMessage"] = $"{quantity} item(s) added to the cart.";
            }

            // Save the cart back to the session
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Details", new { id });
        }
    }
}
