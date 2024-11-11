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
    [Route("api/[controller]")]
    [Authorize]
    public class FixReservationController : Controller
    {

        private readonly IAuthenticationService _authService;
        private readonly IFixReservationRepository _reservationRepository;
        private readonly IDeskRepository _deskRepository;

        public FixReservationController
            (
            IAuthenticationService authService,
            IFixReservationRepository reservationRepository,
            IDeskRepository deskRepository)
        {
            _authService = authService;
            _reservationRepository = reservationRepository;
            _deskRepository = deskRepository;
        }







    
        /// <summary>
        /// get user's fixReservations confirmed(accepted or rejected)
        /// </summary>
        /// <returns></returns>
        [HttpGet("confirmed")]
        public async Task<IActionResult> GetFixReservationsConfirmed()
        {
            var user = await _authService.GetUser(HttpContext);

            if (user == null)
            {
                return Unauthorized(new {status=PreservedStringMessage.FailedStatus ,status_code=401 , message =PreservedStringMessage.UserNotValid});
            }
            var fixReservations = await _reservationRepository.GetFixReservationConfirmedByUserID(user.UserID.ToString());
            string jsonStr = Helper.SerializeObject<IEnumerable<FixReservation>>((IEnumerable<FixReservation>)fixReservations);
            return Ok(jsonStr);
        }


        /// <summary>
        /// get user's fixReservation requests (not confirmed by acceptance or rejection)
        /// </summary>
        /// <returns></returns>
        [HttpGet("request")]
        public async Task<IActionResult> GetFixReservationRequests()
        {
            var user = await _authService.GetUser(HttpContext);

            if (user == null)
            {
                return Unauthorized(new { status = PreservedStringMessage.FailedStatus, status_code = 401, message = PreservedStringMessage.UserNotValid });
            }
            var fixRequests = await _reservationRepository.GetFixReservationRequestByUserID(user.UserID);
            string jsonStr = Helper.SerializeObject<IEnumerable<FixReservation>>((IEnumerable<FixReservation>)fixRequests);
            return Ok(jsonStr);
        }






        

        /// <summary>
        /// send  fix reservation request.
        /// </summary>
        /// <param name="reservationModel"></param>
        /// <returns></returns>
        [HttpPost("request")]
        public async Task<IActionResult> CreateFixReservationRequest([FromBody] ReservationModel reservationModel)
        {
            var user = await _authService.GetUser(HttpContext);
            if (user == null)
            {
                return Unauthorized(new { status = PreservedStringMessage.FailedStatus, status_code = 401, message = PreservedStringMessage.UserNotValid });
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


        /// <summary>
        /// get fix reservation by given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>fix reservation</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetReservationByID(int id)
        {

            var reservation = await _reservationRepository.GetFixReservationByID(id);
            if (reservation == null)
            {
                return NotFound(new {status= PreservedStringMessage.FailedStatus , status_code=404 , message=PreservedStringMessage.ReservationNotFound});
            }
            var str = Helper.SerializeObject<FixReservation>((FixReservation)reservation);
            return Ok(str);
        }



      
        /// <summary>
        /// confirm fix reservation with true or false . this endpoint intends to be used for admin rule.
        /// </summary>
        /// <param name="reservationID"></param>
        /// <param name="confRequest"></param>
        /// <returns>reservation  confirmed</returns>
        [HttpPost("request/{reservationID:int}")]
        public async Task<IActionResult> ConfirmFixReservation(int reservationID, [FromBody] ConfirmReservationRequest confRequest)
        {
            var isAdmin = await _authService.AuthenticateAdmin(HttpContext);
            if (isAdmin == false)
            {
                return Unauthorized(new { status = PreservedStringMessage.FailedStatus, status_code = 401, message = PreservedStringMessage.UserHasNoAdminRole });
            }

            var reservation = await _reservationRepository.GetFixReservationByID(reservationID);
            if (reservation == null)
            {
                return NotFound(new { status = PreservedStringMessage.FailedStatus, status_code = 404, message = PreservedStringMessage.ReservationNotFound });
            }

            // avoid booking same desk for another user in booked period

            if (await IsDeskReserved(reservation) )
            {
                return BadRequest(new { status = PreservedStringMessage.FailedStatus, status_code = 400, message = PreservedStringMessage.DeskAlreadyReserved });
            }

            await _reservationRepository.ConfirmFixReservation(reservationID, confRequest.isConfirmed);
            var str = Helper.SerializeObject(reservation);
            return Ok(str);
        }




  
        /// <summary>
        /// get all fix resevation that not confirmed( accepted or rejected) for all user .this endpoint intends to be used for admin rule
        /// </summary>
        /// <returns> all fix reservation requests</returns>
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
