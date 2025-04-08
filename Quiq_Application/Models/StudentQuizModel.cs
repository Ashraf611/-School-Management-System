using System.ComponentModel.DataAnnotations;

namespace Quiq_Application.Models
{
    public class StudentQuizModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int StudentQuizId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int StudentId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public decimal Score { get; set; }




        public bool Status { get; set; }



        public bool Completed { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int QuizeId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int CourseId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public DateOnly AttemptDate { get; set; }
    }
}
