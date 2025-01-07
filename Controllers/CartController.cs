//using Group13_GroupProject.Models;
using Microsoft.AspNetCore.Mvc;
using Group13_GoodRoots.Data;
using Group13_GoodRoots.Models;
using Group13_GoodRoots.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Group13_GoodRoots.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _ctx;

        public CartController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            // Retrieve the cart from session
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Calculate subtotal, tax, and total
            var subtotal = cart.Sum(item => item.Quantity * (item.Product.IsSale && item.Product.SalePrice.HasValue ? item.Product.SalePrice.Value : item.Product.Price));
            var tax = subtotal * 0.13m; // Assuming a 13% tax rate
            var total = subtotal + tax;

            // Store calculations in ViewData for the view
            ViewData["Subtotal"] = subtotal;
            ViewData["Tax"] = tax;
            ViewData["Total"] = total;

            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            // Fetch product details from database
            var product = _ctx.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product == null || quantity <= 0)
            {
                TempData["Error"] = "Invalid product or quantity.";
                return RedirectToAction("Details", "Product", new { id = productId });
            }

            // Retrieve the cart from session or initialize a new one
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Check if the product is already in the cart
            var existingCartItem = cart.FirstOrDefault(c => c.Product.ProductId == productId);
            if (existingCartItem != null)
            {
                if (existingCartItem.Quantity + quantity <= product.StockQuantity)
                {
                    existingCartItem.Quantity += quantity;
                }
                else
                {
                    TempData["Error"] = "Requested quantity exceeds stock availability.";
                    return RedirectToAction("Details", "Product", new { id = productId });
                }
            }
            else
            {
                if (quantity <= product.StockQuantity)
                {
                    cart.Add(new CartItem { Product = product, Quantity = quantity });
                }
                else
                {
                    TempData["Error"] = "Requested quantity exceeds stock availability.";
                    return RedirectToAction("Details", "Product", new { id = productId });
                }
            }

            // Save the updated cart to session
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            // Success message
            TempData["Success"] = $"{quantity} item(s) added to cart.";
            return RedirectToAction("Details", "Product", new { id = productId });
        }

        public IActionResult RemoveFromCart(int productId)
        {
            // Retrieve the cart from session
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Remove the product from the cart
            cart.RemoveAll(c => c.Product.ProductId == productId);

            // Save the updated cart to session
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            TempData["Success"] = "Item removed from cart.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, string action)
        {
            // Retrieve the cart from session
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var cartItem = cart.FirstOrDefault(c => c.Product.ProductId == productId);

            if (cartItem != null)
            {
                if (action == "increment" && cartItem.Quantity < cartItem.Product.StockQuantity)
                {
                    cartItem.Quantity++;
                }
                else if (action == "decrement" && cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    TempData["Error"] = "Invalid operation or quantity.";
                }
            }

            // Save the updated cart to session
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }
    }
}
