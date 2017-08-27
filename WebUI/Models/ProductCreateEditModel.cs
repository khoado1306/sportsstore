using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class ProductCreateEditModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Please enter a product name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Please enter a description")]
        [StringLength(25,ErrorMessage ="Maximum is 25 characters")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive value")]
        public decimal Price { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        [Range(1,int.MaxValue, ErrorMessage ="Please choose a category")]
        public int SelectedCategoryId { get; set; }
        public List<Category> Categories { get; set; }
        public Category Category { get; set; }
    }
}