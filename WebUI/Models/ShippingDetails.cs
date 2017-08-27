using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class ShippingDetails
    {
        
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the first address line")]
        [Display(Name="Line 1")]
        public string Line1 { get; set; }
        [Display(Name = "Line 2")]
        public string Line2 { get; set; }
        [Display(Name = "Line 3")]
        public string Line3 { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(100,ErrorMessage ="Only 100 characters allowed")]
        public string EmailAddress { get; set; }
        [DataType(DataType.PhoneNumber)]
        [StringLength(20,MinimumLength = 5, ErrorMessage ="Phone can only be 5 to 20 numberss")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter a state name")]
        public string State { get; set; }
        public string Zip { get; set; }
        [Required(ErrorMessage = "Please enter a country name")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
    }
}
