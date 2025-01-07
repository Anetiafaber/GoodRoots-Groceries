﻿// <auto-generated />
using System;
using Group13_GoodRoots.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Group13_GoodRoots.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241207203948_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Group13_GoodRoots.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Fruits"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "Vegetables"
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryName = "Dairy"
                        },
                        new
                        {
                            CategoryId = 4,
                            CategoryName = "Bakery"
                        });
                });

            modelBuilder.Entity("Group13_GoodRoots.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Group13_GoodRoots.Models.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Group13_GoodRoots.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSale")
                        .HasColumnType("bit");

                    b.Property<string>("LongDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            Image = "apple.jpg",
                            IsSale = true,
                            LongDescription = "Crisp and juicy apples, perfect for a healthy snack. They are versatile and can be used in pies, crisps, and other desserts.",
                            Price = 1.50m,
                            ProductName = "Apple",
                            SalePrice = 1.20m,
                            ShortDescription = "Fresh red apples",
                            StockQuantity = 100
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1,
                            Image = "orange.jpg",
                            IsSale = false,
                            LongDescription = "These sweet and tangy oranges are packed with vitamin C. They are perfect for snacking, juicing, or adding to fruit salads.",
                            Price = 1.20m,
                            ProductName = "Orange",
                            ShortDescription = "Sweet oranges",
                            StockQuantity = 100
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 1,
                            Image = "blueberry.jpg",
                            IsSale = true,
                            LongDescription = "Sweet, juicy blueberries, perfect for snacking or baking. They are rich in antioxidants and add a burst of flavor to any dish.",
                            Price = 3.00m,
                            ProductName = "Blueberry",
                            SalePrice = 2.50m,
                            ShortDescription = "Fresh blueberries",
                            StockQuantity = 50
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 1,
                            Image = "strawberry.jpg",
                            IsSale = false,
                            LongDescription = "Bright red strawberries with a sweet, refreshing taste. Perfect for eating fresh, adding to desserts, or making jams.",
                            Price = 2.50m,
                            ProductName = "Strawberry",
                            ShortDescription = "Juicy strawberries",
                            StockQuantity = 70
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 2,
                            Image = "carrot.jpg",
                            IsSale = true,
                            LongDescription = "Fresh, orange carrots that are rich in vitamins. Ideal for snacking, roasting, or adding to salads and soups.",
                            Price = 0.90m,
                            ProductName = "Carrot",
                            SalePrice = 0.70m,
                            ShortDescription = "Crunchy carrots",
                            StockQuantity = 200
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 2,
                            Image = "cucumber.jpg",
                            IsSale = true,
                            LongDescription = "Cool and crisp cucumbers perfect for salads, sandwiches, or just snacking. They have a mild, refreshing taste and high water content.",
                            Price = 0.80m,
                            ProductName = "Cucumber",
                            SalePrice = 0.60m,
                            ShortDescription = "Fresh cucumbers",
                            StockQuantity = 150
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 2,
                            Image = "capsicum.jpg",
                            IsSale = false,
                            LongDescription = "Sweet and crunchy capsicums available in red, yellow, and green. Ideal for salads, stir-fries, and grilling.",
                            Price = 1.50m,
                            ProductName = "Capsicum",
                            ShortDescription = "Colorful capsicums",
                            StockQuantity = 80
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 2,
                            Image = "eggplant.jpg",
                            IsSale = true,
                            LongDescription = "Purple, tender eggplants that are perfect for grilling, roasting, or adding to Mediterranean dishes. They have a mild and slightly smoky flavor.",
                            Price = 1.30m,
                            ProductName = "Eggplant",
                            SalePrice = 1.20m,
                            ShortDescription = "Fresh eggplants",
                            StockQuantity = 60
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryId = 3,
                            Image = "cheese.jpg",
                            IsSale = true,
                            LongDescription = "Smooth, creamy cheese that is perfect for cooking or snacking. It melts beautifully and can be used in a variety of dishes, from pasta to pizza.",
                            Price = 5.00m,
                            ProductName = "Cheese",
                            SalePrice = 4.50m,
                            ShortDescription = "Delicious cheese",
                            StockQuantity = 50
                        },
                        new
                        {
                            ProductId = 10,
                            CategoryId = 3,
                            Image = "milk.jpg",
                            IsSale = true,
                            LongDescription = "Rich and creamy milk sourced from local farms. It’s perfect for drinking, making smoothies, or adding to your coffee and cereals.",
                            Price = 2.00m,
                            ProductName = "Milk",
                            SalePrice = 1.70m,
                            ShortDescription = "Fresh milk",
                            StockQuantity = 100
                        },
                        new
                        {
                            ProductId = 11,
                            CategoryId = 3,
                            Image = "butter.jpg",
                            IsSale = false,
                            LongDescription = "Smooth, creamy butter that’s perfect for cooking, baking, or spreading on toast. It adds a rich flavor to both savory and sweet dishes.",
                            Price = 3.50m,
                            ProductName = "Butter",
                            ShortDescription = "Creamy butter",
                            StockQuantity = 60
                        },
                        new
                        {
                            ProductId = 12,
                            CategoryId = 4,
                            Image = "bread.jpg",
                            IsSale = false,
                            LongDescription = "Soft, freshly baked bread that is perfect for sandwiches or as a side to your meals. It’s made with high-quality ingredients for a soft, airy texture.",
                            Price = 1.50m,
                            ProductName = "Bread",
                            ShortDescription = "Fresh bread",
                            StockQuantity = 80
                        },
                        new
                        {
                            ProductId = 13,
                            CategoryId = 4,
                            Image = "cupcake.jpg",
                            IsSale = false,
                            LongDescription = "Delicious, moist cupcakes topped with creamy frosting. Perfect for parties or as a treat for yourself, available in various flavors.",
                            Price = 2.50m,
                            ProductName = "Cupcake",
                            ShortDescription = "Sweet cupcakes",
                            StockQuantity = 40
                        },
                        new
                        {
                            ProductId = 14,
                            CategoryId = 4,
                            Image = "cake.jpg",
                            IsSale = false,
                            LongDescription = "Freshly baked cakes in a variety of flavors, perfect for celebrations. Whether it’s a birthday or an anniversary, these cakes are sure to delight.",
                            Price = 15.00m,
                            ProductName = "Cake",
                            ShortDescription = "Delicious cakes",
                            StockQuantity = 20
                        },
                        new
                        {
                            ProductId = 15,
                            CategoryId = 4,
                            Image = "croissant.jpg",
                            IsSale = false,
                            LongDescription = "Flaky, buttery croissants, baked fresh every day. These make a perfect breakfast or a savory snack, with a golden, crisp exterior and soft interior.",
                            Price = 2.00m,
                            ProductName = "Croissant",
                            ShortDescription = "Buttery croissants",
                            StockQuantity = 60
                        });
                });

            modelBuilder.Entity("Group13_GoodRoots.Models.UserDetails", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Group13_GoodRoots.Models.Order", b =>
                {
                    b.HasOne("Group13_GoodRoots.Models.UserDetails", "UserDetails")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("Group13_GoodRoots.Models.OrderItem", b =>
                {
                    b.HasOne("Group13_GoodRoots.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Group13_GoodRoots.Models.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Group13_GoodRoots.Models.Product", b =>
                {
                    b.HasOne("Group13_GoodRoots.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Group13_GoodRoots.Models.UserDetails", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Group13_GoodRoots.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Group13_GoodRoots.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Group13_GoodRoots.Models.Product", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Group13_GoodRoots.Models.UserDetails", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}