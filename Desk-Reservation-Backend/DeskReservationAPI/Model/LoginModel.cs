using System.ComponentModel.DataAnnotations;

namespace DeskReservationAPI.Model
{
    public class LoginModel
    {
        [Required, MinLength(1)]
        [EmailAddress]
        public string Email { set; get; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_])(?=.*[a-zA-Z]).{8,}$",
     ErrorMessage = "Password must be at least 8 characters long, contain one uppercase letter and one special character.")]
        public string Password { set; get; }
    }
}
