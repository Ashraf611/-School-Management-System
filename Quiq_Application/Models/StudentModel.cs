using System.ComponentModel.DataAnnotations;

namespace Quiq_Application.Models
{
    public class StudentModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int StudentId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int UserId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int CourseId { get; set; }

        public bool Validation { get; set; }


    }
}
