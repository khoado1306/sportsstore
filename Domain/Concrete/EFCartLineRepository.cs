using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFCartLineRepository : ICartLineRepository
    {
        private EFDbContext _context;

        public EFCartLineRepository(EFDbContext context)
        {
            _context = context;
        }
        public void CreateLine(CartLine cartLine)
        {
            CartLine line = _context.CartLines.Find(cartLine.CartLineId);
            if (line == null)
            {
                _context.CartLines.Add(line);
            }
            else {
                line.Quantity = cartLine.Quantity;
            }
            _context.SaveChanges();
        }


        public List<CartLine> GetAllCartLines()
        {
            return _context.CartLines.OrderBy(c => c.CartLineId).ToList();
        }

        public List<CartLine> GetByCartId(int cartId)
        {
            return _context.CartLines.Where(c => c.CartLineId == cartId).ToList();
        }
    }
}
