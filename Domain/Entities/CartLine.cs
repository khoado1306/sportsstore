using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class CartLine
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int CartLineId { get; set; }
        public virtual int  ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual int Quantity { get; set; }
        public virtual int CartId { get; set; }
    }
}
