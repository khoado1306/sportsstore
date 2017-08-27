using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFCartRepository : ICartRepository
    {
        private EFDbContext _context;

        public EFCartRepository(EFDbContext context)
        {
            _context = context;
        }

        public void AddCart(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public IEnumerable<Cart> GetAll()
        {
            return _context.Carts;
        }
    }
}
