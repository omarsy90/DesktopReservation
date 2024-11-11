using System.ComponentModel.DataAnnotations;

namespace DeskReservationAPI.Model
{
    public class ReservationModel
    {
      
        [Required]
        public int DeskID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DtStart { get; set; }

      
        [Required]
        public bool isFavourited { get; set; }
    }

    public class FlexReservationModel : ReservationModel
    {
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DtEnd { get; set; }

    }
}
