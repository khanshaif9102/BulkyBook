using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BulkyBook.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;

        
        //[ValidateNever]
        [Range(0, 100, ErrorMessage = "Range must be between 0 and 100")]
        [Display(Name = "Display Order")]
        public int? DisplayOrder { get; set; }
    }
}
