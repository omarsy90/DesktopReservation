using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskReservationAPI.Model
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public  string UserID { get; set;}

        [ForeignKey("DeskID")]
        public virtual Desk Desk { get; set; }
        public  int DeskID { get;  set; }

        public string CommentTxt { get; set; }
        public DateTime CommentedAt { get; set; }

    }
}
