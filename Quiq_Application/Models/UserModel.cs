using System.ComponentModel.DataAnnotations;

namespace Quiq_Application.Models
{
    public class UserModel
    {
        [Required(AllowEmptyStrings = false,ErrorMessage ="this field is required")]
        public int UserId { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        [StringLength(50)]
        public string FirstName { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        [StringLength(50)]
        public string LastName { get; set; } 




        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } 




        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string Password { get; set; } 



        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public bool Gender { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int UserAddressId { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public int UserTypeId { get; set; }

        public string? UserTypeName { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "this field is required")]
        public bool UserValidation { get; set; }

    }
}
