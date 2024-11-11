using DeskReservationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DeskReservationAPI.Repository
{

    public interface IFlexReservationRepository : IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetFlexReservationByUserID(string userID);
        Task<IEnumerable<Reservation>> GetFlexReservation();
        Task<Reservation> AddFlexReservation(FlexReservation flexReservation);
        Task<Reservation> GetFlexReservationByID(int reservationID);
        Task<bool> DeleteFlexreservation(int reservationID);
        
    }

    public class FlexReservationRepository :  ReservationRepository,IFlexReservationRepository
    {
       
        public FlexReservationRepository(ApplicationDBContext dBContext):base(dBContext) 
        {
                _dbContext = dBContext;
        }

        public async Task<IEnumerable<Reservation>> GetFlexReservationByUserID(string userID)
        {
            var flexReservations =  _dbContext.FlexReservations.Include(re => re.Desk).Include(re => re.Desk.Office).Where(re => re.UserID == userID);
            return flexReservations;
        }


        public async Task<IEnumerable<Reservation>> GetFlexReservation()
        {
            var flexReservation = await _dbContext.FlexReservations.Include(re => re.Desk).Include(re => re.Desk.Office).ToListAsync();
            return flexReservation;
        }


        public async Task<Reservation> AddFlexReservation(FlexReservation flexReservation)
        {
            await _dbContext.FlexReservations.AddAsync(flexReservation);
            await _dbContext.SaveChangesAsync();
            return flexReservation;
        }

        public async Task<Reservation> GetFlexReservationByID(int reservationID)
        {
            var flexReservatin = await _dbContext.FlexReservations.Include(re => re.Desk).Include(re => re.Desk.Office).FirstOrDefaultAsync(re => re.ReservationID == reservationID);
            return flexReservatin;
        }

        public async Task<bool> DeleteFlexreservation(int reservationID)
        {
            var resservation = await _dbContext.FlexReservations.FirstOrDefaultAsync(re=>re.ReservationID == reservationID);
            if(resservation == null)
            {
                return false;
            }

            _dbContext.FlexReservations.Remove(resservation);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
