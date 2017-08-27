using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Infrastructure;

namespace Domain.Concrete
{
    public class EFDbContext : IdentityDbContext<Customer>
    {
        public EFDbContext() : base("DefaultConnection")
        {
        }
        static EFDbContext()
        {
            Database.SetInitializer<EFDbContext>(new CustomDbInitializer());
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<Cart> Carts { get; set; }        
    }
}
