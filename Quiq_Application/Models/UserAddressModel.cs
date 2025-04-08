using System.ComponentModel.DataAnnotations;

namespace Quiq_Application.Models
{
    public class UserAddressModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int UserAddressId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public string UserAddressName { get; set; } 
    }
}
