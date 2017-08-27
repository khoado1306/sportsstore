using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface ICategoryRepository
    {
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        Category GetById(int id);
        IList<Category> GetAll();
    }
}
