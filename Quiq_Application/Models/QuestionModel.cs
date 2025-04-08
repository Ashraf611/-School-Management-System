using System.ComponentModel.DataAnnotations;

namespace Quiq_Application.Models
{
    public class QuestionModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int QuestionsId { get; set; }




        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public string Text { get; set; } = null!;



        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public string FistOption { get; set; } = null!;


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public string SecontOption { get; set; } = null!;


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public string ThirdOption { get; set; } = null!;


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public string FourthOption { get; set; } = null!;


        [Required(AllowEmptyStrings = false, ErrorMessage = "none of these")]
        public int Answer { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "wrong grade")]
        public int Grade { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int CourseId { get; set; }

        public string? CourseName { get; set; }

        public int? Actual { get; set; }

    }
}
