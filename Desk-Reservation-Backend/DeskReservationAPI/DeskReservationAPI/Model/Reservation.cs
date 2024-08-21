using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskReservationAPI.Model
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }
        public virtual User User { get; set;}
        public int UserID { get; set; }

        public virtual Desk Desk { get; set; }
        public int DeskID { get; set; }

        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateStart { get; set; }

        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }
        public bool isFavourited { get; set; }


    }



    public class FlexReservation : Reservation
    {

    }



    public class Fixreservation :Reservation
    {
        public bool IsConfirmed { get; set; }
    }
}
