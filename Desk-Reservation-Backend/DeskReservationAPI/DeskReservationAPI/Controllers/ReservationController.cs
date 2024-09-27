using DeskReservationAPI.Repository;
using DeskReservationAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskReservationAPI.Controllers
{

    [ApiController]
    [Route("controller")]
    [Authorize]
    public class ReservationController : Controller
    {

        private readonly AuthenticationService _authService;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController
            (
            AuthenticationService authService,
            IReservationRepository reservationRepository)
        {
            _authService = authService;
            _reservationRepository = reservationRepository;
        }


        // get flix reservation for user
        [HttpGet("flix/{userID:guid}")]
        public async Task<IActionResult> GetFlixReservation()
        {

            var user = await _authService.GetUser(HttpContext);
            if (user == null)
            {
                return NotFound();
            }
            var flixreservations = await _reservationRepository.GetFlexReservationByUserID(user.UserID.ToString());
            return Ok(flixreservations);


        }


        //get fix reservation confirmed for user
       [HttpGet("fix/confirmed/{userID:guid}")]
        public async Task<IActionResult> GetFixReservation()
        {
            var user = await _authService.GetUser(HttpContext);

            if (user == null)
            {
                return NotFound();
            }
            var fixReservations = await _reservationRepository.GetFixReservationConfirmedByUserID(user.UserID.ToString());
            return Ok(fixReservations);
        }


        //// get fix reservation requests for user
        [HttpGet("fix/request/{userID:guid}")]
        public async Task<IActionResult> GetFixReservationRequestsByUser()
        {


            var user = await _authService.GetUser(HttpContext);
            if (user == null)
            {
                return NotFound();
            }
            var fixRequests = await _reservationRepository.GetFixReservationRequestByUserID(user.UserID.ToString());
            return Ok(fixRequests);

        }


        // get fix reservation requests  
        [HttpGet("fix/request")]
        public async Task<IActionResult> GetFixReservationRequests()
        {
            var isAdmin = await _authService.AuthenticateAdmin(HttpContext);

            if (isAdmin == false)
            {
                return Unauthorized();
            }
            var fixRequests = await _reservationRepository.GetFixReservationRequest();
            return Ok(fixRequests);
        }


    }
}
