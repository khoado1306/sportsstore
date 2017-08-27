using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private EFDbContext _context;

        public EFCategoryRepository(EFDbContext context)
        {
            _context = context;
        }
        public void CreateCategory(Category category)
        {
            Category entry = _context.Categories.
                Where(c => c.CategoryName.Equals(category.CategoryName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (entry != null) {
                throw new ArgumentException("category");
            }
            _context.Categories.Add(entry);
            _context.SaveChanges();
        }

        public IList<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.Find(id);
        }

        public void UpdateCategory(Category category)
        {
            Category entry = _context.Categories.Find(category.CategoryId);
            if (entry != null) {
                int duplicate = _context.Categories.Where(c => c.CategoryName.Equals(category.CategoryName, StringComparison.InvariantCultureIgnoreCase)).Count();
                if (duplicate > 0)
                {
                    throw new ArgumentException("category");
                }
                else {
                    entry.CategoryName = category.CategoryName;
                    _context.SaveChanges();
                }
            }
        }
    }
}
