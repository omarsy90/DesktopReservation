using DeskReservationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DeskReservationAPI.Repository
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetFixReservation();
        Task<IEnumerable<Reservation>> GetFixReservationConfirmedByUserID(string userID);
        Task<IEnumerable<Reservation>> GetFlexReservation();
        Task<IEnumerable<Reservation>> GetFlexReservationByUserID(string userID);
        Task<IEnumerable<Reservation>> GetFixReservationRequest();

        Task<IEnumerable<Reservation>> GetFixReservationRequestByUserID(string userID);
    }

    public class ReservationRepository : IReservationRepository
    {

        private ApplicationDBContext _dbcontext;
        public ReservationRepository(ApplicationDBContext applicationDBContext)
        {
            _dbcontext = applicationDBContext;
        }

        public async Task<IEnumerable<Reservation>> GetFlexReservationByUserID(string userID)
        {
            var flexReservations = await _dbcontext.FlexReservations.Include(re => re.Desk).Include(re => re.Desk.Office).Where(re => re.UserID.ToString() == userID ).ToListAsync();
            return flexReservations;
        }

        public async Task<IEnumerable<Reservation>> GetFixReservationConfirmedByUserID(string userID)
        {
            var fixReservations = await _dbcontext.FixReservations.Include(re => re.Desk).Include(re => re.Desk.Office).Where(re => re.UserID.ToString() == userID && re.IsConfirmed ==true).ToListAsync();
            return fixReservations;
        }

        public async Task<IEnumerable<Reservation>> GetFlexReservation()
        {
            var flexReservation = await _dbcontext.FlexReservations.Include(re => re.Desk).Include(re => re.Desk.Office).ToListAsync();
            return flexReservation;
        }

        public async Task<IEnumerable<Reservation>> GetFixReservation()
        {
            var fixReservation = await _dbcontext.FixReservations.Include(re => re.Desk).Include(re => re.Desk.Office).ToListAsync();
            return fixReservation;
        }


        public async Task<IEnumerable<Reservation>> GetFixReservationRequest()
        {
            var fixReservationsRequests = await _dbcontext.FixReservations.Include(re => re.Desk).Include(re => re.Desk.Office).Where(re => re.IsConfirmed == null).ToListAsync();
            return fixReservationsRequests;
        }

        public async Task<IEnumerable<Reservation>> GetFixReservationRequestByUserID(string userID)
        {
            var fixReservationsRequests = await _dbcontext.FixReservations.Include(re => re.Desk).Include(re => re.Desk.Office).Where(re=>re.UserID.ToString() ==userID && re.IsConfirmed == null).ToListAsync();
            return fixReservationsRequests;
        }
    }
}
