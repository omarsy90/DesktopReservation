using DeskReservationAPI.Model;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeskReservationAPI.Repository
{
    public interface IFixReservationRepository  : IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetFixReservation();
        Task<IEnumerable<Reservation>> GetFixReservationConfirmedByUserID(string userID);
        
       
        Task<IEnumerable<Reservation>> GetFixReservationRequest();

        Task<IEnumerable<Reservation>> GetFixReservationRequestByUserID(string userID);
        Task<bool> ConfirmFixReservation(int reservationID,bool isConfirmed);
        Task<Reservation> GetFixReservationByID(int reservationID);
       
       
        Task<Reservation> AddFixReservation(FixReservation fixReservation);
        Task< IEnumerable<Reservation>> GetFixReservationConfirmed();
        Task<IEnumerable<FixReservation>> GetConfirmedFixReservationByDeskID(int deskID);
    }

    public class FixReservationRepository : ReservationRepository, IFixReservationRepository 
    {
        public FixReservationRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
        }

        public async Task<IEnumerable<Reservation>> GetFixReservationConfirmedByUserID(string userID)
        {
           
            var notConfirmedReservations = await _dbContext.FixReservations.Include(re=>re.Desk).Include(re=>re.Desk.Office).Where(re =>  re.IsConfirmed != null ).ToListAsync();

           return  notConfirmedReservations.Where(re => re.UserID.ToString().ToUpper() == userID.ToUpper());
          
        }

       

        public async Task<IEnumerable<Reservation>> GetFixReservation()
        {
            var fixReservation = await _dbContext.FixReservations.Include(re => re.Desk).Include(re => re.Desk.Office).ToListAsync();
            return fixReservation;
        }


        public async Task<IEnumerable<Reservation>> GetFixReservationRequest()
        {
            var fixReservationsRequests = await _dbContext.FixReservations.Include(re => re.Desk).Include(re => re.Desk.Office).Where(re => re.IsConfirmed == null).ToListAsync();
            return fixReservationsRequests;
        }

        public async Task<IEnumerable<Reservation>> GetFixReservationRequestByUserID(string userID)
        {
            var fixReservationsRequests = await _dbContext.FixReservations.Include(re => re.Desk).Include(re => re.Desk.Office).Where(re=>re.UserID.ToString() ==userID && re.IsConfirmed == null).ToListAsync();
            return fixReservationsRequests;
        }

        public async Task<bool> ConfirmFixReservation(int reservationID, bool isConfirmed)
        {
            var reservation = await _dbContext.FixReservations.FirstAsync(re => re.ReservationID == reservationID);
            if (reservation == null)
            {
                return false;
            }
            reservation.IsConfirmed = isConfirmed;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Reservation> GetFixReservationByID(int reservationID)
        {
            var fixReservatin =  await _dbContext.FixReservations.Include(re=>re.Desk).Include(re=>re.Desk.Office).FirstOrDefaultAsync(re=>re.ReservationID == reservationID);
            return fixReservatin;
        }

     

        public async Task<Reservation> AddFixReservation(FixReservation fixReservation)
        {
            await _dbContext.FixReservations.AddAsync(fixReservation);
           await _dbContext.SaveChangesAsync();
            return await _dbContext.FixReservations.AsNoTracking().Include(fr => fr.Desk).Include(fr => fr.Desk.Office).FirstOrDefaultAsync(fr => fr.ReservationID == fixReservation.ReservationID);
        }

       

        public async Task< IEnumerable< Reservation>> GetFixReservationConfirmed()
        {
            var  reservationsConfirmed = await _dbContext.FixReservations.Where(re=>re.IsConfirmed !=null).ToListAsync();
            return reservationsConfirmed;
        }

        public async Task<IEnumerable<FixReservation>> GetConfirmedFixReservationByDeskID(int deskID)
        {
            return  await _dbContext.FixReservations.Where(re=>re.DeskID == deskID && re.IsConfirmed == true).ToListAsync();
        }
    }
}
