using System.ComponentModel.DataAnnotations;

namespace Quiq_Application.Models
{
    public class CategoryModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int CategoryId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public string CategoryName { get; set; } = null!;

    }
}
