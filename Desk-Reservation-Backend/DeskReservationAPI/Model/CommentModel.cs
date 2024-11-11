using System.ComponentModel.DataAnnotations;

namespace DeskReservationAPI.Model
{
    public class CommentModel
    {
        [Required]
        [Range(1,int.MaxValue)]
        public int DeskID { get; set; }

        [Required]
        [StringLength(100)]
        public string CommentTxt { get; set; }
    }
}
