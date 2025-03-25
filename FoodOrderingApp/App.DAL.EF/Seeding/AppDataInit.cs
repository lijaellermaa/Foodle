using App.Domain;
using App.Domain.Identity;
using Helpers.Base.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Seeding;

public static class AppDataInit
{
    public static void MigrateDatabase(AppDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DropDatabase(AppDbContext context)
    {
        context.Database.EnsureDeleted();
    }

    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        var identitySeeder = new IdentitySeeder(userManager, roleManager);
        identitySeeder.SeedIdentity();
    }

    public static void SeedAppData(AppDbContext context)
    {
        SeedAppDataOrder(context);
        context.SaveChanges();
    }

    private static void SeedAppDataOrder(AppDbContext context)
    {
        // Define multiple product types
        var productTypes = new List<ProductType>
        {
            new ProductType { Name = "Beverage" },
            new ProductType { Name = "Entree" },
            new ProductType { Name = "Dessert" },
            new ProductType { Name = "Appetizer" },
            new ProductType { Name = "Salad" }
        };

        // Define restaurants
        var restaurants = new List<Restaurant>
        {
            new Restaurant
            {
                Name = "Grill King",
                PhoneNumber = "555-1010",
                OpenTime = "09:00",
                CloseTime = "22:00",
                ImageUrl = "restaurant-1.jpg",
                Location = new Location { Area = "Central", Town = "Metro City", Address = "100 King St" }
            },
            new Restaurant
            {
                Name = "Sushi Corner",
                PhoneNumber = "555-2020",
                OpenTime = "11:00",
                CloseTime = "23:00",
                ImageUrl = "restaurant-2.jpg",
                Location = new Location { Area = "Northside", Town = "Metro City", Address = "200 Queen St" }
            },
            new Restaurant
            {
                Name = "Pasta Palace",
                PhoneNumber = "555-3030",
                OpenTime = "10:00",
                CloseTime = "21:00",
                ImageUrl = "restaurant-3.jpg",
                Location = new Location { Area = "East End", Town = "Metro City", Address = "300 Jack St" }
            },
            new Restaurant
            {
                Name = "Salad Bowl",
                PhoneNumber = "555-4040",
                OpenTime = "08:00",
                CloseTime = "20:00",
                ImageUrl = "restaurant-4.jpg",
                Location = new Location { Area = "Southside", Town = "Metro City", Address = "400 Ace St" }
            },
            new Restaurant
            {
                Name = "Dessert Den",
                PhoneNumber = "555-5050",
                OpenTime = "12:00",
                CloseTime = "24:00",
                ImageUrl = "restaurant-5.jpg",
                Location = new Location { Area = "West Wing", Town = "Metro City", Address = "500 Joker St" }
            }
        };

        // Create multiple products linked to product types and restaurants
        var products = new List<Product>();
        var prices = new List<Price>();
        var random = new Random();
        var productNames = new List<string>
        {
            "Burger", "Sushi", "Pasta", "Caesar Salad", "Chocolate Cake", "Latte", "Soup", "Cheese Plate", "Ice Cream",
            "Steak"
        };

        foreach (var restaurant in restaurants)
        {
            foreach (var productType in productTypes)
            {
                for (var i = 0; i < 6; i++) // Create 6 products per type per restaurant
                {
                    var product = new Product
                    {
                        Name = $"{productNames[random.Next(productNames.Count)]} {productType.Name}",
                        Size = (i % 2 == 0) ? "Large" : "Small",
                        Description = $"Delicious {productType.Name} served at {restaurant.Name}",
                        ImageUrl = $"food-{random.Next(1, 8)}.jpg",
                        ProductType = productType,
                        Restaurant = restaurant
                    };

                    products.Add(product);
                    prices.Add(new Price
                    {
                        Value = random.Next(10, 30),
                        Product = product,
                    });

                }
            }
        }

        // Create an order to use in order items
        var order = new Order
        {
            DeliveryType = DeliveryType.Delivery,
            Status = OrderStatus.Created,
            AppUserId = IdentitySeeder.AdminUser.Id,
            Restaurant = restaurants.First(),
            DeliverTo = IdentitySeeder.AdminUser.Address,
            PaymentMethod = PaymentMethod.CreditCard,
        };

        // Seed order items, one for each product
        var orderItems = products
            .Take(10)
            .Select(p => new OrderItem
            {
                Quantity = (uint)random.Next(1, 5),
                Product = p,
                Price = prices.Where(x => x.ProductId == p.Id).MaxBy(x => x.CreatedAt),
                Order = order,
            }).ToList();

        // Add data to context and save
        context.ProductTypes.AddRange(productTypes);
        context.Restaurants.AddRange(restaurants);
        context.Products.AddRange(products);
        context.Prices.AddRange(prices);
        context.Orders.Add(order);
        context.OrderItems.AddRange(orderItems);
        context.SaveChanges();
    }
}