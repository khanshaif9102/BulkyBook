using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BulkyBook.Models.ViewModels
{
    public class RegisterVM
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Street Address")]
        public string? StreetAddress { get; set; }
        [Display(Name = "City")]
        public string? City { get; set; }
        [Display(Name = "State")]
        public string? State { get; set; }
        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}
