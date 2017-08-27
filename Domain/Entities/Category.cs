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
    public class Category
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int CategoryId { get; set; }
        [Required(ErrorMessage = "Please enter a category name")]
        public virtual string CategoryName { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
