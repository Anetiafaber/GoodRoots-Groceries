using Group13_GoodRoots.Data;
using Group13_GoodRoots.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group13_GoodRoots.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // Display Products
        public IActionResult ProductManagement()
        {
            var products = _context.Products.Include(p => p.Category).ToList();
            return View(products); // View: Views/Admin/ProductManagement.cshtml
        }

        // Edit Product - GET
        public IActionResult EditProduct(int id)
        {
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound(); // Product not found
            }

            ViewBag.Categories = _context.Categories.ToList(); // Populate categories for dropdown
            return View(product); // Pass product to view for editing
        }

        // Edit Product - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                // Log validation errors to see why the model is invalid
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }

                // Repopulate categories and return to the form with errors
                ViewBag.Categories = _context.Categories.ToList();
                return View(product);
            }

            if (ModelState.IsValid)
            {
                // Ensure the product exists before updating
                var existingProduct = _context.Products.Find(product.ProductId);
                if (existingProduct == null)
                {
                    return NotFound(); // Return 404 if the product is not found
                }

                // Update the product fields
                existingProduct.ProductName = product.ProductName;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.Price = product.Price;
                existingProduct.StockQuantity = product.StockQuantity;

                try
                {
                    // Explicitly mark the entity as modified if necessary
                    _context.Entry(existingProduct).State = EntityState.Modified;
                    _context.SaveChanges(); // Save changes to the database
                    return RedirectToAction("ProductManagement"); // Redirect after saving
                }
                catch (Exception ex)
                {
                    // Log the exception details (optional)
                    Console.WriteLine($"Error saving product: {ex.Message}");
                    ModelState.AddModelError("", "There was an error saving the product.");
                }
            }

            // Repopulate categories and return to the form with errors
            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }


        // Delete Product - GET
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product); // View: Views/Admin/DeleteProduct.cshtml
        }

        // Delete Product - POST
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction("ProductManagement");
        }
        // Display Categories
        public IActionResult CategoryManagement()
        {
            // Fetch all categories
            var categories = _context.Categories.ToList();
            return View(categories); // View: Views/Admin/CategoryManagement.cshtml
        }

        // Add New Product - GET
        public IActionResult AddProduct()
        {
            // Pass categories for dropdown selection
            ViewBag.Categories = _context.Categories.ToList();
            return View(); // View: Views/Admin/AddProduct.cshtml
        }

        // Add New Product - POST
        // Add New Product - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                // Save the new product to the database
                _context.Products.Add(product);
                _context.SaveChanges();

                // Redirect to Product Management page
                return RedirectToAction("ProductManagement");
            }

            // Repopulate categories in case of errors and return the form with errors
            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }
       
        // View all users
        public async Task<IActionResult> UserManagement()
        {
            var users = _userManager.Users.ToList();  // Get all users
            return View(users);
        }

        // Delete user (GET)
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);  // Find the user by ID
            if (user == null)
            {
                return NotFound();  // Return 404 if user not found
            }

            return View(user);  // Return the view with user info
        }

        // Confirm delete (POST)
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);  // Delete the user
                if (result.Succeeded)
                {
                    return RedirectToAction("UserManagement");  // Redirect to the User Management page
                }
                // Handle failure (if any error occurs during deletion)
                ModelState.AddModelError("", "Error deleting user.");
            }

            return RedirectToAction("UserManagement");
        }
    }

}
