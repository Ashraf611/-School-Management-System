using System.ComponentModel.DataAnnotations;

namespace Quiq_Application.Models
{
    public class TeacherModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int TeacherId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int UserId { get; set; }
    }
}
