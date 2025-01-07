using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Group13_GoodRoots.Models;
using Group13_GoodRoots.Data;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _ctx;

    public OrderController(ApplicationDbContext context)
    {
        _ctx = context;
    }

    public IActionResult OrderConfirmation(int orderId)
    {
        var order = _ctx.Orders
            .Include(o => o.UserDetails)
            .ThenInclude(u => u.User) // includes IdentityUser to access Email
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product) // include Product details for order items           
            .FirstOrDefault(o => o.OrderId == orderId);

        if (order == null)
        {
            return NotFound();
        }

        // Log details of the order items for debugging
        foreach (var item in order.OrderItems)
        {
            Console.WriteLine($"ProductId: {item.ProductId}, ProductName: {item.Product.ProductName}, Quantity: {item.Quantity}, UnitPrice: {item.UnitPrice}");
        }


        decimal totalAmount = order.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice);
        decimal tax = totalAmount * 0.13m;
        decimal orderTotal = totalAmount + tax;

        // passes these values to the view via ViewData
        ViewData["Order-TotalAmount"] = totalAmount.ToString("C");
        ViewData["Order-Tax"] = tax.ToString("C");
        ViewData["Order-Total"] = orderTotal.ToString("C");


        return View(order);
    }
}
