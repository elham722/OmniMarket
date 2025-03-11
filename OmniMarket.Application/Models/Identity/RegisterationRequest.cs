
namespace OmniMarket.Application.Models.Identity
{
    public class RegisterationRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one letter and one number")]
        public string Password { get; set; }
    }
}
