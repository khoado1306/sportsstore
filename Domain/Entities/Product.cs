using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue =false)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int ProductID { get; set; }

        [Required(ErrorMessage ="Please enter a product name")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [DataType(DataType.MultilineText)]
        public virtual string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage ="Please enter a positive value")]
        public virtual decimal Price { get; set; }
        public virtual int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category
        {
            get; set;
        }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
