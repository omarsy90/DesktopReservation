using DeskReservationAPI.Model;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DeskReservationAPI.Utility
{
    public static class Helper
    {

        public static List<Reservation> GetOverlappedReservations(IEnumerable<Reservation> reservations, Reservation reservation)
        {

            
            var overlappedReservations = from res in reservations
                                         where res.DateEnd >= reservation.DateStart
                                         && res.DateStart <= reservation.DateEnd
                                         select res;
            return overlappedReservations?.ToList();
        }

        public static List<Reservation> GetOverlappedReservations(IEnumerable<Reservation> reservations, FlexReservationModel reservation)
        {


            var overlappedReservations = from res in reservations
                                         where res.DateEnd >= reservation.DtStart
                                         && res.DateStart <= reservation.DtEnd
                                         select res;
            return overlappedReservations?.ToList();
        }






        public static string SerializeObject<T>(T? obj)
        {

            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            var option = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            string str = JsonSerializer.Serialize<T>(obj, option);
            return str;

        }
    }
}
