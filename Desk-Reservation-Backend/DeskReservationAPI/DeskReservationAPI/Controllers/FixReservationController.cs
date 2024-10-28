using DeskReservationAPI.Model;
using DeskReservationAPI.Repository;
using DeskReservationAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DeskReservationAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FixReservationController : Controller
    {

        private readonly AuthenticationService _authService;
        private readonly IFixReservationRepository _reservationRepository;
        private readonly IDeskRepository _deskRepository;

        public FixReservationController
            (
            AuthenticationService authService,
            IFixReservationRepository reservationRepository,
            IDeskRepository deskRepository)
        {
            _authService = authService;
            _reservationRepository = reservationRepository;
            _deskRepository = deskRepository;
        }







        //get fix reservation confirmed(true/false) for user
        [HttpGet("confirmed")]
        public async Task<IActionResult> GetFixReservationConfirmed()
        {
            var user = await _authService.GetUser(HttpContext);

            if (user == null)
            {
                return Unauthorized();
            }
            var fixReservations = await _reservationRepository.GetFixReservationConfirmedByUserID(user.UserID.ToString());
            string jsonStr = Helper.SerializeObject<IEnumerable<FixReservation>>((IEnumerable<FixReservation>)fixReservations);
            return Ok(jsonStr);
        }





        // get fix reservation requests  for user
        [HttpGet("request")]
        public async Task<IActionResult> GetFixReservationRequests()
        {
            var user = await _authService.GetUser(HttpContext);

            if (user == null)
            {
                return Unauthorized();
            }
            var fixRequests = await _reservationRepository.GetFixReservationRequest();
            string jsonStr = Helper.SerializeObject<IEnumerable<FixReservation>>((IEnumerable<FixReservation>)fixRequests);
            return Ok(jsonStr);
        }






        // send request to fix reservation
        [HttpPost("request")]
        public async Task<IActionResult> CreateFixReservationRequest([FromBody] ReservationModel reservationModel)
        {
            var user = await _authService.GetUser(HttpContext);
            if (user == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var desk = await _deskRepository.GetDeskByID(reservationModel.DeskID);
            if (desk == null)
            {
                return NotFound(reservationModel.DeskID);

            }

            // check  if the desk not reseved  in duration of  reservation requested 
            if( await IsDeskReserved(reservationModel))
            {
                return BadRequest(new { status = PreservedStringMessage.FailedStatus, status_code = 400, message = PreservedStringMessage.DeskAlreadyReserved });
            }
           
            //check if user has already reservation in duration requested 
            if( await DoesUserHasReservation(reservationModel,user.UserID.ToUpper()))
            {
                return BadRequest(new { status = PreservedStringMessage.FailedStatus, status_code = 400, message = PreservedStringMessage.UserHasAlreadyReservationInTimeRequested });
            }

            FixReservation fixReservation = new FixReservation()
            {
                UserID = user.UserID.ToString(),
                DeskID = reservationModel.DeskID,
                DateStart = DateTime.Parse(reservationModel.DtStart.ToString("yyyy-MM-dd")),
                DateEnd = DateTime.Parse(reservationModel.DtStart.AddDays(90-1).ToString("yyyy-MM-dd")),
                isFavourited = reservationModel.isFavourited
            };

            var reservation = await _reservationRepository.AddFixReservation(fixReservation);
            var str = Helper.SerializeObject<FixReservation>((FixReservation)reservation);
            return Ok(reservation);

        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetReservationByID(int id)
        {

            var reservation = await _reservationRepository.GetFixReservationByID(id);
            if (reservation == null)
            {
                return NotFound(reservation);
            }
            var str = Helper.SerializeObject<FixReservation>((FixReservation)reservation);
            return Ok(str);
        }



        // confirm  fix reservation request with true or false  
        [HttpPost("request/{reservationID:int}")]
        public async Task<IActionResult> ConfirmFixReservation(int reservationID, [FromBody] ConfirmReservationRequest confRequest)
        {
            var isAdmin = await _authService.AuthenticateAdmin(HttpContext);
            if (isAdmin == false)
            {
                return Unauthorized();
            }

            var reservation = await _reservationRepository.GetFixReservationByID(reservationID);
            if (reservation == null)
            {
                return NotFound(reservationID);
            }

            // avoid booking same desk for another user in booked period

            if (await IsDeskReserved(reservation) )
            {
                return BadRequest(new { status = PreservedStringMessage.FailedStatus, status_code = 400, message = PreservedStringMessage.DeskAlreadyReserved });
            }

            await _reservationRepository.ConfirmFixReservation(reservationID, confRequest.isConfirmed);
            var str = Helper.SerializeObject<FixReservation>((FixReservation)reservation);
            return Ok(reservation);
        }




        // get fix reservation  that not confirmed yet for all user
        [HttpGet("request/all")]
        public async Task<IActionResult> GetAllFixReservationRequests()
        {
            var isAdmin = await _authService.AuthenticateAdmin(HttpContext);

            if (!isAdmin)
            {
                return Unauthorized();
            }
            var fixReservations = await _reservationRepository.GetFixReservationRequest();
            string jsonStr = Helper.SerializeObject<IEnumerable<FixReservation>>((IEnumerable<FixReservation>)fixReservations);
            return Ok(jsonStr);
        }


        private async Task< bool> IsDeskReserved(ReservationModel model)
        {
            var confirmedReservations = await _reservationRepository.GetResevationsByDesk(model.DeskID);
            var overlappedReservations = Helper.GetOverlappedReservations(confirmedReservations, new Reservation
            { DeskID = model.DeskID, DateStart = model.DtStart, DateEnd = model.DtStart.AddDays(90 - 1) });

            if (overlappedReservations != null && overlappedReservations.Count > 0)
            {
                return true;
            }
            return false;
        }


        private async Task<bool> IsDeskReserved(Reservation reservation)
        {
            var confirmedReservations = await _reservationRepository.GetResevationsByDesk(reservation.DeskID);
            var overlappedReservations = Helper.GetOverlappedReservations(confirmedReservations, reservation);

            if (overlappedReservations != null && overlappedReservations.Count > 0)
            {
                return true;
            }
            return false;
        }

        private async Task<bool>DoesUserHasReservation(ReservationModel model, string userID)
        {
            var userReservations = await _reservationRepository.GetReservationsbyUserID(userID.ToUpper());
            var overlappedUserReservation = Helper.GetOverlappedReservations(userReservations, new Reservation
            { DeskID = model.DeskID, DateStart = model.DtStart, DateEnd = model.DtStart.AddDays(90 - 1) });

            if (overlappedUserReservation != null && overlappedUserReservation.Count > 0)
            {
                return true;
            }

            return false;
        }
      
    }



   

    public record ConfirmReservationRequest
    {
        public bool isConfirmed { get; set; }
    }
}
