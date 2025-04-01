using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Account
{
    public class AccountRegistrationViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email field is required")]
        public string Email { get; set; } = null!;
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone field is required")]
        [RegularExpression("^[0-9]{10,11}$", ErrorMessage = "Phone number requires minimum of 10 characters and has a max length cap at 11 character")]
        public string Phone { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password field is required")]
        [MinLength(8, ErrorMessage = "Password must contain at least 8 characters")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$", ErrorMessage = "Password must contain at least 1 uppercase character with 1 lowercase character and one number")]
        public string Password { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fullname field is required")]
        public string Fullname { get; set; } = null!;

        [Required(ErrorMessage = "You must select your gender")]
        public Gender Gender { get; set; } = default;
    }
}
