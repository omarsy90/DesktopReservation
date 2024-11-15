using System.ComponentModel.DataAnnotations;

namespace DeskReservationAPI.Model
{
    public class RegisterModel
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }


        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Required, StringLength(100)]
        public string UserName { get; set; }

        [Required, StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required."), StringLength(50)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_])(?=.*[a-zA-Z]).{8,}$",
ErrorMessage = "Password must be at least 8 characters long, contain one uppercase letter and one special character.")]

        public string Password { get; set; }

        [Required, StringLength(50)]
        public string Department {  get; set; }

        
    }
}
