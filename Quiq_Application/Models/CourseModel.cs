using System.ComponentModel.DataAnnotations;

namespace Quiq_Application.Models
{
    public class CourseModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int CourseId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public string CourseName { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public string Description { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int TeacherId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int CatigoryId { get; set; }
    }
}
