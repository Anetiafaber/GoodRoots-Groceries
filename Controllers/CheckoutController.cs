using Group13_GoodRoots.Data;
using Group13_GoodRoots.Models;
using Group13_GoodRoots.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Group13_GoodRoots.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _ctx;

        // constructor with DI for UserManager and DbContext
        public CheckoutController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _ctx = context;
        }



        [HttpGet]
        //public IActionResult Checkout()
        public IActionResult Index()
        {
            // gets logged-in user ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            // if the user is not logged in (userId is null or empty), redirect to login page
            if (string.IsNullOrEmpty(userId))
            {
                // redirects to the login page of the Account controller
                //return RedirectToAction("Login", "Account", new { area = "Identity" });
                return Redirect("/Identity/Account/Login");
            }

            // retrieve IdentityUser
            var user = _userManager.FindByIdAsync(userId).Result;

            // retrieves user details from the UserDetails table
            var userDetails = _ctx.UserDetails.FirstOrDefault(u => u.UserId == userId);

            // retrieves cart items from the session
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");

            if (cartItems == null || !cartItems.Any())
            {
                TempData["Error"] = "Your cart is empty!";
                return RedirectToAction("Index", "Cart");
            }

            // calculates the subtotal (sum of prices * quantities)
            decimal subtotal = cartItems.Sum(item => item.Quantity *
                (item.Product.IsSale && item.Product.SalePrice.HasValue ?
                item.Product.SalePrice.Value : item.Product.Price));

            // assuming a 13% tax rate
            decimal tax = subtotal * 0.13m;

            // calculates total (subtotal + tax)
            decimal total = subtotal + tax;

            // stores calculations in ViewData for the view
            ViewData["Checkout-Subtotal"] = subtotal.ToString("C");
            ViewData["Checkout-Tax"] = tax.ToString("C");
            ViewData["Checkout-Total"] = total.ToString("C");


            // creates a new instance of CheckoutViewModel with cart data
            var viewModel = new CheckoutViewModel
            {
                CustomerDetails = new CustomerDetailsViewModel
                {
                    FirstName = userDetails?.FirstName,
                    LastName = userDetails?.LastName,
                    Street = userDetails?.Street,
                    City = userDetails?.City,
                    Province = userDetails?.Province,
                    PostCode = userDetails?.PostCode,
                    Country = userDetails?.Country,
                    PhoneNumber = userDetails?.PhoneNumber
                },
                PaymentDetails = new PaymentDetailsViewModel(),
                Email = user?.Email

            };

            return View(viewModel);
        }



        [HttpPost]
        public IActionResult Pay(CheckoutViewModel model)
        {
            // Log whether ModelState is valid
            Console.WriteLine($"ModelState is valid-1: {ModelState.IsValid}");

            Console.WriteLine("Logging ModelState errors:");

            Console.WriteLine("Logging CheckoutViewModel properties:");
            Console.WriteLine($"Email: {model.Email}");


            if (ModelState.IsValid)
            {
                // get logged-in user ID
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // retrieves user details
                var userDetails = _ctx.UserDetails.FirstOrDefault(u => u.UserId == userId);

                // If user details exist, update the phone number if it's different
                if (userDetails != null)
                {
                    userDetails.PhoneNumber = model.CustomerDetails.PhoneNumber;
                    // updates the phone number in the database
                    _ctx.SaveChanges();
                }
                else
                {
                    // if user details don't exist, creates new entry
                    var newUserDetails = new UserDetails
                    {
                        UserId = userId,
                        FirstName = model.CustomerDetails.FirstName,
                        LastName = model.CustomerDetails.LastName,
                        Street = model.CustomerDetails.Street,
                        City = model.CustomerDetails.City,
                        Province = model.CustomerDetails.Province,
                        PostCode = model.CustomerDetails.PostCode,
                        Country = model.CustomerDetails.Country,
                        PhoneNumber = model.CustomerDetails.PhoneNumber
                    };
                    _ctx.UserDetails.Add(newUserDetails);
                }

                // saves userDetails to the database using DbContext
                _ctx.SaveChanges();

                // retrieves cart items from the session
                var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
                if (cartItems == null || !cartItems.Any())
                {
                    TempData["Error"] = "Your cart is empty!";
                    return RedirectToAction("Index", "Cart");
                }


                // Create a new order
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    TotalAmount = cartItems.Sum(item => item.Quantity *
                                (item.Product.IsSale && item.Product.SalePrice.HasValue ?
                                item.Product.SalePrice.Value : item.Product.Price)),
                    OrderStatus = "Paid"
                };
                _ctx.Orders.Add(order);
                _ctx.SaveChanges();

                // adds order items and updates stock quantity
                foreach (var cartItem in cartItems)
                {
                    // NEW - deducts stock, and creates order
                    var product = _ctx.Products.FirstOrDefault(p => p.ProductId == cartItem.Product.ProductId);
                    if (product != null)
                    {
                        if (product.StockQuantity < cartItem.Quantity)
                        {
                            TempData["Error"] = $"Not enough stock for {product.ProductName}. Available quantity: {product.StockQuantity}";
                            return RedirectToAction("Index", "Cart");
                        }

                        // deducts stock
                        product.StockQuantity -= cartItem.Quantity;

                        // creates order item
                        var orderItem = new OrderItem
                        {
                            OrderId = order.OrderId,
                            ProductId = product.ProductId,
                            Quantity = cartItem.Quantity,
                            UnitPrice = product.IsSale && product.SalePrice.HasValue
                                ? product.SalePrice.Value
                                : product.Price
                        };
                        _ctx.OrderItems.Add(orderItem);
                    }

                }

                // saves changes to db 
                _ctx.SaveChanges();

                // clears the cart from the session
                HttpContext.Session.SetObjectAsJson("Cart", new List<CartItem>());

                TempData["Success"] = "Payment successful! Your order has been placed.";

                // redirects to order confirmation page
                //return RedirectToAction("OrderConfirmation", "Order");
                return RedirectToAction("OrderConfirmation", "Order", new { orderId = order.OrderId });

            }

            // Log whether ModelState is valid
            Console.WriteLine($"ModelState is valid-2: {ModelState.IsValid}");

            // NEW - retrieves cart items from the session
            var cartItemsForView = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");

            if (cartItemsForView == null || !cartItemsForView.Any())
            {
                TempData["Error"] = "Your cart is empty!";
                return RedirectToAction("Index", "Cart");
            }

            // NEW - recalculates subtotal, tax, and total for the order summary
            decimal subtotal = cartItemsForView.Sum(item => item.Quantity *
                (item.Product.IsSale && item.Product.SalePrice.HasValue
                    ? item.Product.SalePrice.Value
                    : item.Product.Price));
            decimal tax = subtotal * 0.13m;
            decimal total = subtotal + tax;

            // NEW - updates ViewData for the order summary
            ViewData["Checkout-Subtotal"] = subtotal.ToString("C");
            ViewData["Checkout-Tax"] = tax.ToString("C");
            ViewData["Checkout-Total"] = total.ToString("C");

            // NEW - returns the view with the current model to persist entered data
            return View("Index", model);
        }
    }
}
