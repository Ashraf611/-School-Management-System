using System.ComponentModel.DataAnnotations;

namespace Quiq_Application.Models
{
    public class AdminModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int AdminId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int UserId { get; set; }
    }
}
