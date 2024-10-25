using DeskReservationAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskReservationAPI.Utility;

namespace DeskReservationAPITest
{
    public class Helper_GetOverlappedReservations_Test
    {
        
        List<Reservation> _reservations;
        ReservationModel _reservationModel; //reservation A
        Reservation _reservationB;

        
        public Helper_GetOverlappedReservations_Test()
        {
                _reservationModel = new ReservationModel();
            _reservationB = new Reservation() ;
            _reservations = new List<Reservation> { _reservationB };

            _reservationModel.DtStart = GetRundomDate();
            _reservationModel.DtEnd = AddDays(_reservationModel.DtStart, 5);

        }


        // DateStartB > DateStartA       && DateEndB > DateEndndA
        [Fact]
        public async Task GetOverlappedReservations_DateStartOfBBiggerThanDateStartofAAndDateEndOfBBiggerThanDateEndOfA_ReturnB()
        {
            //Arrange
           

            _reservationB.DateStart = AddDays(_reservationModel.DtStart, 1);
            _reservationB.DateEnd = AddDays(_reservationModel.DtEnd, -1);
            //Action

           var overlappedList = DeskReservationAPI.Utility.Helper.GetOverlappedReservations(_reservations, _reservationModel);
            //Assert

            Assert.True(overlappedList.ToList().Count > 0);
            Assert.True(overlappedList.ToList()[0] == _reservationB);

        }


        [Fact]
        // DateStartB > DateStartA       && DateEndB < DateEndndA
        public async Task GetOverlappedReservations_DateStartOfBBiggerThanDateStartofAAndDateEndOfBSmallerThanDateEndOfA_ReturnB()
        {
            //Arrange

            _reservationB.DateStart = AddDays(_reservationModel.DtStart, 1);
            _reservationB.DateEnd = AddDays(_reservationModel.DtEnd, 1);
            //Action
            var overlappedList = DeskReservationAPI.Utility.Helper.GetOverlappedReservations(_reservations, _reservationModel);
            //Assert

            Assert.True(overlappedList.ToList().Count > 0);
            Assert.True(overlappedList.ToList()[0] == _reservationB);


        }

        // DateStartB < DateStartA       && DateEndB < DateEndndA
        [Fact]
        public async Task GetOverlappedReservations_DateStartOfBSmallerThanDateStartofAAndDateEndOfBSmallerThanDateEndOfA_ReturnB()
        {
            //Arrange
            _reservationB.DateStart = AddDays(_reservationModel.DtStart, -1);
            _reservationB.DateEnd = AddDays(_reservationModel.DtEnd, -1);

            //Action
            var overlappedList = DeskReservationAPI.Utility.Helper.GetOverlappedReservations(_reservations, _reservationModel);
            //Assert

            Assert.True(overlappedList.ToList().Count > 0);
            Assert.True(overlappedList.ToList()[0] == _reservationB);

        }


        // DateStartB < DateStartA       && DateEndB > DateEndndA
        [Fact]
        public async Task GetOverlappedReservations_DateStartOfBSmallerThanDateStartofAAndDateEndOfBBiggerThanDateEndOfA_ReturnB()
        {
            //Arrange
            _reservationB.DateStart = AddDays(_reservationModel.DtStart, -1);
            _reservationB.DateEnd = AddDays(_reservationModel.DtEnd, 1);
            //Action 
            var overlappedList = DeskReservationAPI.Utility.Helper.GetOverlappedReservations(_reservations, _reservationModel);
            //Assert

            Assert.True(overlappedList.ToList().Count > 0);
            Assert.True(overlappedList.ToList()[0] == _reservationB);

        }


        // DateStartB < DateStartA       && DateEndB < DateStartA
        [Fact]
        public async Task GetOverlappedReservations_DateStartOfBSmallerThanDateStartofAAndDateEndOfBSmallerThanDateStartOfA_ReturnNoReservation()
        {

            //Arrange
            _reservationB.DateStart = AddDays(_reservationModel.DtStart, -2);
            _reservationB.DateEnd = AddDays(_reservationModel.DtStart, -1);
            //Action
            var overlappedList = DeskReservationAPI.Utility.Helper.GetOverlappedReservations(_reservations, _reservationModel);
            //Assert

            Assert.True(overlappedList.ToList().Count == 0);
        }


        // DateStartB > DateEndA       &&  DateEndB > DateEndA
        [Fact]
        public async Task GetOverlappedReservations_DateStartOfBBiggerThanDateEndOfAAndDateEndOfBBiggerThanDateEndOfA_ReturnNoReservation()
        {
            //Arrange
            _reservationB.DateStart = AddDays(_reservationModel.DtEnd, 1);
            _reservationB.DateEnd = AddDays(_reservationModel.DtEnd, 2);
            //Action
            var overlappedList = DeskReservationAPI.Utility.Helper.GetOverlappedReservations(_reservations, _reservationModel);
            //Assert

            Assert.True(overlappedList.ToList().Count == 0);


        }

       

        private DateTime AddDays(DateTime dt ,int daysCount )
        {
          var newDt=  dt.AddDays(daysCount);
            return newDt;

        }

        private DateTime GetRundomDate()
        {
            var dt = DateTime.Now;
          var dtString = dt.ToString("yyyy-MM-dd");
           return  DateTime.Parse(dtString);
        }




    }
}
