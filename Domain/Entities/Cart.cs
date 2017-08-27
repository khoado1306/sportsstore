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
    public class Cart
    {
        public Cart()
        {
            LinesCollection = new List<CartLine>();
            CreatedDate = DateTime.Now.ToLocalTime();
        }
        [HiddenInput(DisplayValue = false)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int CartId { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual decimal TotalPrice { get; private set; }
        public virtual List<CartLine> LinesCollection { get; set; }
        public virtual string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public void AddItem(Product product, int quantity)
        {
            CartLine line = LinesCollection
                            .Where(p => p.Product.ProductID == product.ProductID)
                            .FirstOrDefault();
            if (line == null)
            {
                LinesCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Product product)
        {
            LinesCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }
        public decimal ComputeTotalValue()
        {
            TotalPrice = LinesCollection.Sum(e => e.Product.Price * e.Quantity);
            return TotalPrice;
        }
        public void Clear()
        {
            LinesCollection.Clear();
        }
    }
}
