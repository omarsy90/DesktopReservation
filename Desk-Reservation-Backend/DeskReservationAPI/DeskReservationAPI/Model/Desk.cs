using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskReservationAPI.Model
{
    public class Desk
    {

        [Key]
        public int DeskID { set; get; }
        public string Label {  set; get; }
        public string Map { set; get; }  // image url
        
        public virtual Office Office { set; get; }
        public int OfficeID { set; get; }

        public virtual List<Equipment> Equipments { set; get; }
        
    }
}
