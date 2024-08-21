using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskReservationAPI.Model
{
    public class Equipment
    {
        [Key]
        public int EquipmentID {  get; set; }
        public string feature { get; set; }    
        public List<Desk> Desks { get; set; }

    }
}
