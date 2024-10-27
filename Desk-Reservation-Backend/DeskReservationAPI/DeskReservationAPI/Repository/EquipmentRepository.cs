using DeskReservationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DeskReservationAPI.Repository
{
    public interface IEquipmentRepository
    {
        Task<Equipment> AddEquipment(Equipment equipment);
        Task<Equipment> GetEquipmentByName(string equipmentName);
    }

    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly ApplicationDBContext _dBContext;

        public EquipmentRepository(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Equipment> GetEquipmentByName(string equipmentName)
        {
            return await _dBContext.Equipments.FirstOrDefaultAsync(eq => eq.Feature.ToUpper() == equipmentName.ToUpper());
        }

        public async Task<Equipment> AddEquipment(Equipment equipment)
        {
            await _dBContext.Equipments.AddAsync(equipment);
            await _dBContext.SaveChangesAsync();
            return equipment;
        }
    }
}
