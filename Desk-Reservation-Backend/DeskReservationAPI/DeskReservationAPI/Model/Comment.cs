using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskReservationAPI.Model
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        public virtual User User { get; set; }
        public  int UserID { get; set;}
        public virtual Desk Desk { get; set; }
        public  int DeskID { get;  set; }

        public string CommentTxt { get; set; }
        public DateTime CommentedAt { get; set; }

    }
}
