using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface ICartLineRepository
    {
        List<CartLine> GetAllCartLines();
        List<CartLine> GetByCartId(int cartId);
        void CreateLine(CartLine cartLine);
    }
}
