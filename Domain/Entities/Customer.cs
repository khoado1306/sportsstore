using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer : IdentityUser
    {
        public virtual string FullName { get; set; }
        public virtual string Address { get; set; }
        public virtual IList<Cart> Carts { get; set; }
    }
}
