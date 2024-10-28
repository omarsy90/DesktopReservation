using System.ComponentModel.DataAnnotations;

namespace DeskReservationAPI.Model
{
    public class CommentModel
    {
        [Required]
        public int DeskID { get; set; }

        [Required]
        [StringLength(100)]
        public string CommentTxt { get; set; }
    }
}
