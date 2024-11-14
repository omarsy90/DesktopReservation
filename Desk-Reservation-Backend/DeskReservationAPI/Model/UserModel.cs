using System.ComponentModel.DataAnnotations;

namespace DeskReservationAPI.Model
{
    public class UserModel
    {

        [Required]
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

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

    }
}
