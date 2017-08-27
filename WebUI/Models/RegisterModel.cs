using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please enter your username")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Only 5 to 50 characters allowed")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [StringLength(20,MinimumLength =6,ErrorMessage ="Password must be between 6 to 20 characters")]
        [Required(ErrorMessage ="Please enter your password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="The password and confirmation password are not match")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage ="Invalid email address")]
        [StringLength(50,MinimumLength =10,ErrorMessage ="Only 10 to 50 characters allowed")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please enter your full name")]
        [StringLength(50,MinimumLength =5,ErrorMessage ="Only 5 to 50 characters allowed")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter your address")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Only 5 to 50 characters allowed")]
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please enter your phone number")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Only 5 to 20 numbers allowed")]
        public string PhoneNumber { get; set; }
    }
}