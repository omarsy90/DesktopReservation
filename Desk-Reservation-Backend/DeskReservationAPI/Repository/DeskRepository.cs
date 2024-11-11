using DeskReservationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DeskReservationAPI.Repository
{
    public interface IDeskRepository
    {
        Task<Desk> AddDesk(Desk desk);
        Task<Desk> GetDeskByID(int id);
        Task<IEnumerable<Desk>> GetDesks();
        Task<IEnumerable<Desk>> GetDesksByOffice(int officeID);
        Task<Desk> UpdateDesk(int id, Desk newDesk);

        Task<bool> AddEqipment(int deskID, Equipment equip);

    }

    public class DeskRepository : IDeskRepository
    {

        private readonly ApplicationDBContext _dbContext;

        public DeskRepository(ApplicationDBContext dbcontext)
        {
            _dbContext = dbcontext;
        }


        public async Task<Desk> AddDesk(Desk desk)
        {
            await _dbContext.Desks.AddAsync(desk);
            await _dbContext.SaveChangesAsync();
            return desk;
        }
        public async Task<IEnumerable<Desk>> GetDesks()
        {
            return await _dbContext.Desks.Include(d => d.Office).Include(d => d.Equipments).ToListAsync();

        }


        public async Task<Desk> GetDeskByID(int id)
        {
            return await _dbContext.Desks.Include(d => d.Office).Include(d => d.Equipments).FirstOrDefaultAsync(d => d.DeskID == id);
        }

        public async Task<IEnumerable<Desk>> GetDesksByOffice(int officeID)
        {
            return await _dbContext.Desks.Include(d => d.Office).Include(d => d.Equipments).Where(d => d.OfficeID == officeID).ToListAsync();
        }

        public async Task<Desk> UpdateDesk(int id, Desk newDesk)
        {
            Desk desk = await _dbContext.Desks.Include(d=>d.Office).Include(d=>d.Equipments).FirstOrDefaultAsync(d => d.DeskID == id);
            if (desk == null)
            {
                throw new ArgumentException($"desk with given Id : {id} not found");
            }

            desk.Label = newDesk.Label;
            desk.OfficeID = newDesk.OfficeID;
            desk.IsDeskActive = newDesk.IsDeskActive;
            desk.Map = newDesk.Map;
            desk.Equipments = newDesk.Equipments;

           await _dbContext.SaveChangesAsync();
            return desk;

        }

        public async Task<bool> AddEqipment(int deskID, Equipment equip)
        {
            if (equip == null)
            {
                return false;
            }
            var desk = await _dbContext.Desks.Include(d=>d.Equipments).FirstOrDefaultAsync(d => d.DeskID == deskID);
            if (desk == null)
            {
                return false;
            }

            desk.Equipments.Add(equip);
            await _dbContext.SaveChangesAsync();
            return true;

        }
    }
}
