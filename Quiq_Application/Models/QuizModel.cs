using System.ComponentModel.DataAnnotations;

namespace Quiq_Application.Models
{
    public class QuizModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int QuizId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int QuestionsId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "wrong number")]
        
        public int NumberOfQuestions { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int CourseId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public string Name { get; set; } = null!;


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public DateOnly CreatedDate { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public string RoomCode { get; set; } 


        [Required(AllowEmptyStrings = false, ErrorMessage = "wrong mark")]
        
        public int Mark { get; set; }


        public string? CourseName { get; set; } = null!;
    }
}
