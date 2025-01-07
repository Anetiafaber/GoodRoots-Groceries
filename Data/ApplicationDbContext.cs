using Group13_GoodRoots.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Group13_GoodRoots.Data
{
    //public class ApplicationDbContext : DbContext
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // applies default Identity configurations
            base.OnModelCreating(modelBuilder);

            // One-to-Many - Product and Category
            modelBuilder.Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many - Order and User
            modelBuilder.Entity<Order>()
                .HasOne(u => u.UserDetails)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many - Order and OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Order)
                .WithMany(oi => oi.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many - OrderItem and Product
            modelBuilder.Entity<OrderItem>()
                .HasOne(p => p.Product)
                .WithMany(oi => oi.OrderItems)
                //.HasForeignKey(oi => oi.OrderId)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Fruits" },
                new Category { CategoryId = 2, CategoryName = "Vegetables" },
                new Category { CategoryId = 3, CategoryName = "Dairy" },
                new Category { CategoryId = 4, CategoryName = "Bakery" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                // Fruits
                new Product { ProductId = 1, ProductName = "Apple", ShortDescription = "Fresh red apples", LongDescription = "Crisp and juicy apples, perfect for a healthy snack. They are versatile and can be used in pies, crisps, and other desserts.", Price = 1.50m, StockQuantity = 100, Image = "apple.jpg", CategoryId = 1, IsSale = true, SalePrice = 1.20m },
                new Product { ProductId = 2, ProductName = "Orange", ShortDescription = "Sweet oranges", LongDescription = "These sweet and tangy oranges are packed with vitamin C. They are perfect for snacking, juicing, or adding to fruit salads.", Price = 1.20m, StockQuantity = 100, Image = "orange.jpg", CategoryId = 1, IsSale = false, SalePrice = null },
                new Product { ProductId = 3, ProductName = "Blueberry", ShortDescription = "Fresh blueberries", LongDescription = "Sweet, juicy blueberries, perfect for snacking or baking. They are rich in antioxidants and add a burst of flavor to any dish.", Price = 3.00m, StockQuantity = 50, Image = "blueberry.jpg", CategoryId = 1, IsSale = true, SalePrice = 2.50m },
                new Product { ProductId = 4, ProductName = "Strawberry", ShortDescription = "Juicy strawberries", LongDescription = "Bright red strawberries with a sweet, refreshing taste. Perfect for eating fresh, adding to desserts, or making jams.", Price = 2.50m, StockQuantity = 70, Image = "strawberry.jpg", CategoryId = 1, IsSale = false, SalePrice = null },

                // Vegetables
                new Product { ProductId = 5, ProductName = "Carrot", ShortDescription = "Crunchy carrots", LongDescription = "Fresh, orange carrots that are rich in vitamins. Ideal for snacking, roasting, or adding to salads and soups.", Price = 0.90m, StockQuantity = 200, Image = "carrot.jpg", CategoryId = 2, IsSale = true, SalePrice = 0.70m },
                new Product { ProductId = 6, ProductName = "Cucumber", ShortDescription = "Fresh cucumbers", LongDescription = "Cool and crisp cucumbers perfect for salads, sandwiches, or just snacking. They have a mild, refreshing taste and high water content.", Price = 0.80m, StockQuantity = 150, Image = "cucumber.jpg", CategoryId = 2, IsSale = true, SalePrice = 0.60m },
                new Product { ProductId = 7, ProductName = "Capsicum", ShortDescription = "Colorful capsicums", LongDescription = "Sweet and crunchy capsicums available in red, yellow, and green. Ideal for salads, stir-fries, and grilling.", Price = 1.50m, StockQuantity = 80, Image = "capsicum.jpg", CategoryId = 2, IsSale = false, SalePrice = null },
                new Product { ProductId = 8, ProductName = "Eggplant", ShortDescription = "Fresh eggplants", LongDescription = "Purple, tender eggplants that are perfect for grilling, roasting, or adding to Mediterranean dishes. They have a mild and slightly smoky flavor.", Price = 1.30m, StockQuantity = 60, Image = "eggplant.jpg", CategoryId = 2, IsSale = true, SalePrice = 1.20m },

                // Dairy
                new Product { ProductId = 9, ProductName = "Cheese", ShortDescription = "Delicious cheese", LongDescription = "Smooth, creamy cheese that is perfect for cooking or snacking. It melts beautifully and can be used in a variety of dishes, from pasta to pizza.", Price = 5.00m, StockQuantity = 50, Image = "cheese.jpg", CategoryId = 3, IsSale = true, SalePrice = 4.50m },
                new Product { ProductId = 10, ProductName = "Milk", ShortDescription = "Fresh milk", LongDescription = "Rich and creamy milk sourced from local farms. It’s perfect for drinking, making smoothies, or adding to your coffee and cereals.", Price = 2.00m, StockQuantity = 10, Image = "milk.jpg", CategoryId = 3, IsSale = true, SalePrice = 1.70m },
                new Product { ProductId = 11, ProductName = "Butter", ShortDescription = "Creamy butter", LongDescription = "Smooth, creamy butter that’s perfect for cooking, baking, or spreading on toast. It adds a rich flavor to both savory and sweet dishes.", Price = 3.50m, StockQuantity = 60, Image = "butter.jpg", CategoryId = 3, IsSale = false, SalePrice = null },

                // Bakery
                new Product { ProductId = 12, ProductName = "Bread", ShortDescription = "Fresh bread", LongDescription = "Soft, freshly baked bread that is perfect for sandwiches or as a side to your meals. It’s made with high-quality ingredients for a soft, airy texture.", Price = 1.50m, StockQuantity = 80, Image = "bread.jpg", CategoryId = 4, IsSale = false, SalePrice = null },
                new Product { ProductId = 13, ProductName = "Cupcake", ShortDescription = "Sweet cupcakes", LongDescription = "Delicious, moist cupcakes topped with creamy frosting. Perfect for parties or as a treat for yourself, available in various flavors.", Price = 2.50m, StockQuantity = 40, Image = "cupcake.jpg", CategoryId = 4, IsSale = false, SalePrice = null },
                new Product { ProductId = 14, ProductName = "Cake", ShortDescription = "Delicious cakes", LongDescription = "Freshly baked cakes in a variety of flavors, perfect for celebrations. Whether it’s a birthday or an anniversary, these cakes are sure to delight.", Price = 15.00m, StockQuantity = 20, Image = "cake.jpg", CategoryId = 4, IsSale = false, SalePrice = null },
                new Product { ProductId = 15, ProductName = "Croissant", ShortDescription = "Buttery croissants", LongDescription = "Flaky, buttery croissants, baked fresh every day. These make a perfect breakfast or a savory snack, with a golden, crisp exterior and soft interior.", Price = 2.00m, StockQuantity = 60, Image = "croissant.jpg", CategoryId = 4, IsSale = false, SalePrice = null }
            );

        }
        }
}
