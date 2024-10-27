using System.ComponentModel.DataAnnotations;

namespace DeskReservationAPI.Model
{
    public class DeskModel
    {

        public int Id { get; set; }
        [Required]
        public string Label { get; set; }
        public string Map { get; set; }

        [Required]
        public string Office { get; set; }
        [Required]
        public string Equipmentst {  get; set; }

        [Required]
        public bool isActive { get; set; }
    }
}
