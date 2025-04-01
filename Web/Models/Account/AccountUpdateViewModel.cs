using BusinessObject.Enums;

namespace Web.Models.Account
{
    public class AccountUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public Gender Gender { get; set; }
        public Role Role { get; set; }
    }
}
