using System.ComponentModel.DataAnnotations;

namespace DeskReservationAPI.Model
{
    public class UserModel
    {

        [Required, StringLength(50)]
        [EmailAddress]
        public string Email {  get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Department { get; set; }

        [Required(ErrorMessage = "Password is required."), StringLength(50)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_])(?=.*[a-zA-Z]).{8,}$",
ErrorMessage = "Password must be at least 8 characters long, contain one uppercase letter and one special character.")]
        public string Password { get; set; }

    }
}
