using DeskReservationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DeskReservationAPI.Repository
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetResevationsByDesk(int deskId);
        Task<IEnumerable<Reservation>> GetReservationsbyUserID(string userID);
    }

    public abstract class ReservationRepository : IReservationRepository
    {
        protected ApplicationDBContext _dbContext;
        public ReservationRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsbyUserID(string userID)
        {
           
            List<Reservation> reservations = new List<Reservation>();
          var fixReservations = await  _dbContext.FixReservations.Where(re => re.UserID == userID && re.IsConfirmed == true).AsNoTracking().ToListAsync();
            var flexReservations = await _dbContext.FlexReservations.Where(re=>re.UserID == userID).AsNoTracking().ToListAsync();
            reservations.AddRange(fixReservations);
            reservations.AddRange(flexReservations);
            return reservations;
        }

        public async Task<IEnumerable<Reservation>> GetResevationsByDesk(int deskId)
        {
            List<Reservation> reservations = new List<Reservation>();
            var fixReservations = _dbContext.FixReservations.Where(re => re.DeskID == deskId && re.IsConfirmed == true);
            var flexReservations = _dbContext.FlexReservations.Where(re => re.DeskID == deskId);
            reservations.AddRange(fixReservations);
            reservations.AddRange(flexReservations);
            return await Task.FromResult(reservations);
        }
    }
}
