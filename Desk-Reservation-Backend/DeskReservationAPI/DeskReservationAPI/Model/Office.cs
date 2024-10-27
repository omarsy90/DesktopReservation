using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskReservationAPI.Model
{
    public class Office
    {

        [Key]
        public int OfficeID { get; set; }
        public string Name { get; set; }
    }
}
