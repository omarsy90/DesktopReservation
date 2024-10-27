using System.ComponentModel.DataAnnotations;

namespace DeskReservationAPI.Model
{
    public class LoginModel
    {
        [Required, MinLength(1)]
        public string Email { set; get; }

        [Required, MinLength(1)]
        public string Password { set; get; }
    }
}
