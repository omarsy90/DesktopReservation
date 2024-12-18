﻿using DeskReservationAPI.Model;
using DeskReservationAPI.Repository;
using DeskReservationAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskReservationAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FlexReservationController : Controller
    {

        private readonly IAuthenticationService _authService;
        private readonly IFlexReservationRepository _reservationRepository;
        private readonly IDeskRepository _deskRepository;
        public FlexReservationController
            (
            IAuthenticationService authService,
            IFlexReservationRepository reservationRepository,
            IDeskRepository deskRepository)
        {
            _authService = authService;
            _reservationRepository = reservationRepository;
            _deskRepository = deskRepository;
        }


        /// <summary>
        /// returns user's flex reservations
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IActionResult> GetFlexReservation()
        {

            var user = await _authService.GetUser(HttpContext);
            if (user == null)
            {
                return Unauthorized(new {status=PreservedStringMessage.FailedStatus , status_code=401 , message=PreservedStringMessage.UserNotValid});
            }
            var flexreservations = await _reservationRepository.GetFlexReservationByUserID(user.UserID.ToString());
            var str = Helper.SerializeObject(flexreservations);
            return Ok(str);


        }

        /// <summary>
        /// create  flex reservation
        /// </summary>
        /// <param name="reservationModel"></param>
        /// <returns></returns>

        [HttpPost("")]
        public async Task<IActionResult> CreateNewFlexReservation([FromBody] FlexReservationModel reservationModel)
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
            if (reservationModel.DtStart == DateTime.MinValue || reservationModel.DtEnd == DateTime.MinValue)
            {
                return BadRequest(new {status=PreservedStringMessage.FailedStatus,status_code=400, message=PreservedStringMessage.StartDtOrEndDtNotSet});
            }

            if (reservationModel.DtEnd == null || string.IsNullOrEmpty(reservationModel.DtEnd.ToString()))
            {
                return BadRequest(new { status = PreservedStringMessage.FailedStatus, status_code = 400, message = PreservedStringMessage.EndTimeReservationNotRequstedMessage });
            }

            var des = await _deskRepository.GetDeskByID(reservationModel.DeskID);
            if (des == null)
            {
                return NotFound(new {status= PreservedStringMessage.FailedStatus, status_code =404, message = PreservedStringMessage.DeskNotFound});
            }
            // check if desk  reserved in  duration reservation
            if (await IsDeskReserved(reservationModel))
            {
                return BadRequest(new { status = PreservedStringMessage.FailedStatus, status_code = 400, message = PreservedStringMessage.DeskAlreadyReserved });
            }

            // check if the user has already reserved a desk in duration reservation
            if (await DoesUserHasReservation(reservationModel, user.UserID.ToString()))
            {
                return BadRequest(
                    new
                    {
                        status = PreservedStringMessage.FailedStatus,
                        status_code = 400,
                        mmessage = PreservedStringMessage.UserHasAlreadyReservationInTimeRequested + reservationModel.DtStart + " - " + reservationModel.DtEnd
                    }
                    );
            }

            // check if reservation time exceed 3 days -> not allowd :)
            if ((reservationModel.DtEnd.Date - reservationModel.DtStart.Date).TotalDays > 2)
            {
                return BadRequest(new { status = PreservedStringMessage.FailedStatus, status_code = 400, message = PreservedStringMessage.FlexReservationTimeExceed });
            }

            FlexReservation flexReservation = new FlexReservation()
            {
                DeskID = reservationModel.DeskID,
                UserID = user.UserID.ToString(),
                DateStart = DateTime.Parse(reservationModel.DtStart.ToString("yyyy-MM-dd")),
                DateEnd = DateTime.Parse(reservationModel.DtEnd.ToString("yyyy-MM-dd")),
                isFavourited = reservationModel.isFavourited
            };

            var reservation = await _reservationRepository.AddFlexReservation(flexReservation);
            string serialzedReservation = Helper.SerializeObject<Reservation>(reservation);
            return Ok(serialzedReservation);
        }


        /// <summary>
        /// returns flex reservation by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetReservationByID(int id)
        {
            var reservation = await _reservationRepository.GetFlexReservationByID(id);
            if (reservation == null)
            {
                return NotFound(new {status=PreservedStringMessage.FailedStatus, status_code=404, message=PreservedStringMessage.ReservationNotFound});
            };

            var str = Helper.SerializeObject(reservation);
            return Ok(str);
        }


        /// <summary>
        /// remove flex reservation by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFlexReservation(int id)
        {
            var reservation = await _reservationRepository.GetFlexReservationByID(id);
            if (reservation == null)
            {
                return NotFound(new { status = PreservedStringMessage.FailedStatus , status_code=404, message= PreservedStringMessage.ReservationNotFound});
            }

            var user = await _authService.GetUser(this.HttpContext);
            if (user.UserID.ToString().ToUpper() != reservation.UserID.ToUpper())
            {
                return Unauthorized( new { status = PreservedStringMessage.FailedStatus, status_code= 401 , message=PreservedStringMessage.UserNotValid} );
            }

            var cancellingDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            // the cancelling time after  reservation has finished ->  not allowed
            if (reservation.DateEnd < cancellingDate)
            {
                return BadRequest(new {status= PreservedStringMessage.FailedStatus , status_code=400 , message=PreservedStringMessage.CancellingTimeShouldSetBeforeFinishingReservation});
            }

            // the cancelling time during occupation the desk  -> not allowed
            if (reservation.DateEnd >= cancellingDate && reservation.DateStart <= cancellingDate)
            {
                return BadRequest(new {status=PreservedStringMessage.FailedStatus , status_code=400,message = PreservedStringMessage.CancellingTimeNotAllowedDuringOccupation});
            }


            // the cancelling time before resservation has started -> allowed 
            await _reservationRepository.DeleteFlexreservation(id);
            return Ok(new { status = PreservedStringMessage.SuccessStatus, status_code = 200, message = PreservedStringMessage.ReservationSuccessfullyDeleted });
        }

        private async Task<bool> IsDeskReserved(FlexReservationModel model)
        {
            var reservations = await _reservationRepository.GetResevationsByDesk(model.DeskID);
            var overlappedReservations = Helper.GetOverlappedReservations(reservations, model);

            if (overlappedReservations != null && overlappedReservations.Count > 0)
            {
                return true;
            }
            return false;
        }

        private async Task<bool> DoesUserHasReservation(FlexReservationModel model, string userID)
        {
            var userReservations = await _reservationRepository.GetReservationsbyUserID(userID.ToUpper());
            var overlappedUserReservation = Helper.GetOverlappedReservations(userReservations, model);

            if (overlappedUserReservation != null && overlappedUserReservation.Count > 0)
            {
                return true;
            }

            return false;
        }



    }
}
