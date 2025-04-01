using System.ComponentModel.DataAnnotations;

namespace Web.Models.Account
{
    public class AccountLoginViewModel
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
