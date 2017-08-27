using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFProductReposity : IProductRepository
    {
        public EFProductReposity(EFDbContext context)
        {
            _context = context;
        }
        private EFDbContext _context;
        public IEnumerable<Product> Products
        {
            get
            {
                return _context.Products;
            }
        }
        public Product DeleteProduct(int productId)
        {
            Product dbEntry = _context.Products.Find(productId);
            if (dbEntry != null)
            {
                _context.Products.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public Product GetById(int productId)
        {
            Product entry = _context.Products.Find(productId);
            return entry;
        }

        public void SaveProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            Product entry = _context.Products.Find(product.ProductID);
            if (entry != null)
            {
                entry.Description = product.Description;
                entry.Name = product.Name;
                entry.Price = product.Price;
                entry.ImageData = product.ImageData;
                entry.ImageMimeType = product.ImageMimeType;
                entry.CategoryId = product.CategoryId;
            }
            _context.SaveChanges();
        }
    }
}
