using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class CustomDbInitializer : CreateDatabaseIfNotExists<EFDbContext>
    {
        private void CreateCategorySample(EFDbContext context)
        {
            Category category = new Category {
                CategoryName = "Shoes"
            };
            context.Categories.Add(category);
            category = new Category {
                CategoryName = "Clothes"
            };
            context.Categories.Add(category);
            category = new Category
            {
                CategoryName = "Ball"
            };
            context.Categories.Add(category);
            category = new Category
            {
                CategoryName = "Equipment"
            };
            context.Categories.Add(category);
            category = new Category
            {
                CategoryName = "Supplement"
            };
            context.Categories.Add(category);
            context.SaveChanges();
        }
        private void CreateProductSample(EFDbContext context)
        {
            Product product = new Product {
                CategoryId=1,
                Description= "Run strong when the weather heats up ",
                Name = "New Balance Vazee orange",
                Price = 120,
            };
            context.Products.Add(product);
            product = new Product {
                CategoryId=1,
                Description = "Designed for high speed",
                Name = "New Balance vazee black",
                Price = 110,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 1,
                Description = "Shoes that give you a burst of energy with every step.",
               Name = "New Balance vazee rush elite team",
                 Price = 120,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 1,
                Description = "Running shoes",
                Name = "Adidas cloudfoam gray",
                Price = 80,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 1,
                Description = "Running shoes",
                Name = "Adidas NMD blue",
                Price = 180,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 1,
                Description = "Running shoes",
                Name = "Nike red",
                Price = 130,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 1,
                Description = "Running shoes",
                Name = "Micheal Jordan red",
                Price = 200,
            };
            context.Products.Add(product);

            product = new Product
            {
                CategoryId = 2,
                Description = "black hat",
                Name = "Adidas original hat",
                Price = 60,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 2,
                Description = "white hat",
                Name = "nike hat",
                Price = 50,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 2,
                Description = "black t-shirt",
                Name = "Adidas original t-shirt",
                Price = 90,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 2,
                Description = "black t-shirt",
                Name = "nike air t-shirt",
                Price = 80,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 2,
                Description = "gray t-shirt",
                Name = "nike golf t-shirt",
                Price = 100,
            };
            context.Products.Add(product);

            product = new Product
            {
                CategoryId = 3,
                Description = "champion league",
                Name = "2017 champion league ball",
                Price = 70,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 3,
                Description = "premier league",
                Name = "english Premier ball",
                Price = 80,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 3,
                Description = "nba ball",
                Name = "NBA ball",
                Price = 90,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 3,
                Description = "volleyball",
                Name = "Volleyball",
                Price = 20,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 3,
                Description = "world cup ball",
                Name = "brazil world cup ball",
                Price = 120,
            };
            context.Products.Add(product);

            product = new Product
            {
                CategoryId = 4,
                Description = "gym equipment",
                Name = "bench press",
                Price = 360,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 4,
                Description = "gym equipment",
                Name = "dumb bell",
                Price = 85,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 4,
                Description = "gym equipment",
                Name = "back workout equipment",
                Price = 550,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 4,
                Description = "gym equipment",
                Name = "belly workout equipment",
                Price = 260,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 4,
                Description = "gym equipment",
                Name = "leg workout equipment",
                Price = 420,
            };
            context.Products.Add(product);

            product = new Product
            {
                CategoryId = 5,
                Description = "gym supplement",
                Name = "blue sup",
                Price = 120,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 5,
                Description = "gym supplement",
                Name = "red sup",
                Price = 159,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 5,
                Description = "gym supplement",
                Name = "purple sup",
                Price = 130,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 5,
                Description = "gym supplement",
                Name = "green sup",
                Price = 220,
            };
            context.Products.Add(product);
            product = new Product
            {
                CategoryId = 5,
                Description = "gym supplement",
                Name = "supper red sup",
                Price = 300,
            };
            context.Products.Add(product);
            context.SaveChanges();
        }
        private void CreateRoles(EFDbContext context)
        {
            if (!context.Roles.Any(r => r.Name.Equals("StoreAdmin")))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "StoreAdmin" };
                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name.Equals("StoreUser")))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "StoreUser" };
                manager.Create(role);
            }
            context.SaveChanges();
        }
        private void CreateUsers(EFDbContext context)
        {
            var userManager = new UserManager<Customer>(new UserStore<Customer>(context));
            var passwordHash = new PasswordHasher();
            if (!context.Users.Any(u => u.UserName.Equals("administrator")))
            {
                var user = new Customer()
                {
                    UserName = "administrator",
                    Email = "admin@admin.com",
                    PasswordHash = passwordHash.HashPassword("secret")
                };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "StoreAdmin");
            }
            for (int i = 1; i <= 5; i++)
            {
                var user = new Customer()
                {
                    UserName = "customer" + i,
                    Email = "customer" + i + "@.customer" + i + ".com",
                    PasswordHash = passwordHash.HashPassword("123456")
                };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "StoreUser");
            }
            context.SaveChanges();
        }
        protected override void Seed(EFDbContext context)
        {
            CreateRoles(context);
            CreateUsers(context);
            CreateCategorySample(context);
            CreateProductSample(context);
            base.Seed(context);
        }
    }
}
