using Group13_GoodRoots.Data;
using Group13_GoodRoots.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Group13_GoodRoots.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _ctx { get; set; }

        public HomeController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            var featuredProducts = _ctx.Products
                                       .Where(p => p.IsSale == true)
                                       .Take(6)
                                       .ToList();

            var bakedGoods = _ctx.Products
                                  .Where(p => p.CategoryId == 4)
                                  .ToList();

            var viewModel = new HomeViewModel
            {
                FeaturedProducts = featuredProducts,
                BakedGoods = bakedGoods
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
