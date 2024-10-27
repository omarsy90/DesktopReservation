using DeskReservationAPI;
using DeskReservationAPI.Model;
using DeskReservationAPI.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeskReservationAPITest
{
    public class FlexReservationControllerTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        HttpClient _client;
        Utility _utility;
        PasswordEncoder _encoder;
        string FlexReservationEndPoint = Utility.BasicUrl + "/FlexReservation";

        public FlexReservationControllerTest(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _utility = new Utility(_client);
            _encoder = new PasswordEncoder();

        }

        [Fact]
        public async Task CreateNewFlexReservation_UnvalidReservationModel_RetuenbadRequest()
        {
            //Arrange
            LoginModel loginModel = new LoginModel() { Email = SeededData.User1.Email, Password = SeededData.User1Pass };
            var loginResponse = await _utility.Login(loginModel);
            string token = await _utility.ExtractToken(loginResponse);

            FlexReservationModel reservationModel = new FlexReservationModel()
            {
                DeskID = 1,
                DtStart = DateTime.Parse("2025-03-01"),


            };
            //Action
            var res = await CreateFlexReservation(token, reservationModel);

            //Assert
            Assert.True(res.StatusCode == System.Net.HttpStatusCode.BadRequest);


        }

        [Fact]
        public async Task CreateNewFlexReservation_DeskAlreadyBookedInDurationRequested_ReturnBadRequest()
        {

            //Arrange
            LoginModel loginModel = new LoginModel() { Email = SeededData.User1.Email, Password = SeededData.User1Pass };
            var loginResponse = await _utility.Login(loginModel);
            string token = await _utility.ExtractToken(loginResponse);

            FlexReservationModel reservationModel = new FlexReservationModel()
            {
                DeskID = 2,
                DtStart = DateTime.Parse("2024-03-04"),
                DtEnd = DateTime.Parse("2024-03-04").AddDays(3 - 1)


            };
            //Action
            var res = await CreateFlexReservation(token, reservationModel);

            //Assert
            var resContent = await _utility.GetContentFromResponse(res);
            Assert.True(res.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.True(resContent.Contains(PreservedStringMessage.DeskAlreadyReserved));

        }

        [Fact]
        public async Task CreateNewFlexReservation_DeskNotFound_RteuenNotFound()
        {

            //Arrange
            LoginModel loginModel = new LoginModel() { Email = SeededData.User1.Email, Password = SeededData.User1Pass };
            var loginResponse = await _utility.Login(loginModel);
            string token = await _utility.ExtractToken(loginResponse);

            FlexReservationModel reservationModel = new FlexReservationModel()
            {
                DeskID = 0,
                DtStart = DateTime.Parse("2024-03-06"),
                DtEnd = DateTime.Parse("2024-03-06").AddDays(3 - 1)


            };
            //Action
            var res = await CreateFlexReservation(token, reservationModel);

            //Assert

            Assert.True(res.StatusCode == System.Net.HttpStatusCode.NotFound);



        }

        [Fact]
        public async Task CreateNewFlexReservation_UserHasAlreadyReservationInDurationRequested_ReturnBadRequest()
        {
            //Arrange
            LoginModel loginModel = new LoginModel() { Email = SeededData.User1.Email, Password = SeededData.User1Pass };
            var loginResponse = await _utility.Login(loginModel);
            string token = await _utility.ExtractToken(loginResponse);

            FlexReservationModel model = new FlexReservationModel
            {
                DeskID = 4,
                DtStart = DateTime.Parse("2024-03-04"),
                DtEnd = DateTime.Parse("2024-03-05"),
                isFavourited = true
            };

            //action
                var res = await CreateFlexReservation(token, model);
            //Assert

            Assert.True(res.StatusCode == System.Net.HttpStatusCode.BadRequest);
            string resContent =  await _utility.GetContentFromResponse(res);
            Assert.True(resContent.Contains(PreservedStringMessage.UserHasAlreadyReservationInTimeRequested));

        }

        [Fact]
        public async Task CreateNewFlexReservation_ValidReservationModel_ReturnOk()
        {

            //Arrange
            LoginModel loginModel = new LoginModel() { Email = SeededData.User1.Email, Password = SeededData.User1Pass };
            var loginResponse = await _utility.Login(loginModel);
            string token = await _utility.ExtractToken(loginResponse);

            FlexReservationModel reservationModel = new FlexReservationModel()
            {
                DeskID = 2,
                DtStart = DateTime.Parse("2024-03-06"),
                DtEnd = DateTime.Parse("2024-03-06").AddDays(3 - 1)


            };
            //Action
            var res = await CreateFlexReservation(token, reservationModel);

            //Assert

            Assert.True(res.StatusCode == System.Net.HttpStatusCode.OK);
            var resContent = await _utility.GetContentFromResponse(res);
            var returnedReservation = JsonSerializer.Deserialize<Reservation>(resContent);
            Assert.True(returnedReservation.DeskID == reservationModel.DeskID);
            Assert.True(returnedReservation.DateStart == reservationModel.DtStart);
            Assert.True(returnedReservation.DateEnd == reservationModel.DtEnd);
            Assert.True(returnedReservation.UserID.ToString() == SeededData.User1.UserID.ToString());

        }

        [Fact]
        public async Task CreateNewFlexReservation_ReservationTimeExccedThreeDays_ReturnBadRequest()
        {
            //Arrange
            LoginModel loginModel = new LoginModel() { Email = SeededData.User1.Email, Password = SeededData.User1Pass };
            var loginResponse = await _utility.Login(loginModel);
            string token = await _utility.ExtractToken(loginResponse);

            FlexReservationModel reservationModel = new FlexReservationModel()
            {
                DeskID = 2,
                DtStart = DateTime.Parse("2025-06-06"),
                DtEnd = DateTime.Parse("2025-06-09")


            };
            //Action
            var res = await CreateFlexReservation(token, reservationModel);

            //Assert
            Assert.True(res.StatusCode == System.Net.HttpStatusCode.BadRequest);
            var resContent = await _utility.GetContentFromResponse(res);
            Assert.True(resContent.Contains(PreservedStringMessage.FlexReservationTimeExceed));

        }

        public async Task CreateNewFlexReservation_UserHasReservationInTimeRequsted_RetunBadRequest()
        {
            //Arrange
            LoginModel loginModel = new LoginModel() { Email = SeededData.User1.Email, Password = SeededData.User1Pass };
            var loginResponse = await _utility.Login(loginModel);
            string token = await _utility.ExtractToken(loginResponse);

            FlexReservationModel reservationModel = new FlexReservationModel()
            {
                DeskID = 2,
                DtStart = DateTime.Parse("2025-06-06"),
                DtEnd = DateTime.Parse("2025-06-09")


            };
            //Action
            var res = await CreateFlexReservation(token, reservationModel);

            //Assert
            Assert.True(res.StatusCode == System.Net.HttpStatusCode.BadRequest);
            var resContent = await _utility.GetContentFromResponse(res);
            Assert.True(resContent.Contains(PreservedStringMessage.FlexReservationTimeExceed));

        }


        public async Task CreateNewFlexReservation_DeskIsReservedInDuration_ReturnBadRequest()
        {


        }



        [Fact]
        public async Task DeleteFlexReservation_ReservationNotFound_ReturnNotFound()
        {
            // Arrange
            var logResponse = await _utility.Login(new LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1Pass,
            });
            string token = await _utility.ExtractToken(logResponse);

            //Action
            var res = await DeleteFlexReservation(token, -1);

            //Assert
            Assert.True(res.StatusCode == System.Net.HttpStatusCode.NotFound);

        }


        [Fact]
        public async Task DeleteFlexReservation_UserNotOwner_RetuenUnauthorized()
        {
            // Arrange
            var logResponse = await _utility.Login(new LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1Pass,
            });
            string token = await _utility.ExtractToken(logResponse);

            //Action
            var res = await DeleteFlexReservation(token, 5);

            //Assert
            Assert.True(res.StatusCode == System.Net.HttpStatusCode.Unauthorized);

        }

        [Fact]
        public async Task DeleteFlexReservation_CancellingTimeWhenReservationActive_ReturnBadRequest()
        {
            // Arrange
            var logResponse = await _utility.Login(new LoginModel
            {
                Email = SeededData.User2.Email,
                Password = SeededData.User2Pass,
            });
            string token = await _utility.ExtractToken(logResponse);

            FlexReservationModel model = new FlexReservationModel
            {
                DeskID = 4,
                DtStart = DateTime.Now.AddDays(-1),
                DtEnd = DateTime.Now.AddDays(+1),
            };

            var createReservationRes = await CreateFlexReservation(token, model);
            string createReservationResponseContent = await _utility.GetContentFromResponse(createReservationRes);

            Reservation reservation = JsonSerializer.Deserialize<Reservation>(createReservationResponseContent);
            //Action
            var res = await DeleteFlexReservation(token, reservation.ReservationID);

            //Assert
            Assert.True(res.StatusCode == System.Net.HttpStatusCode.BadRequest);



        }

        [Fact]
        public async Task DeleteFlexReservation_CancellingtimeAfterReservationFinished_ReturnBadRequest()
        {
            // Arrange
            var logResponse = await _utility.Login(new LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1Pass,
            });
            string token = await _utility.ExtractToken(logResponse);

            //Action
            var res = await DeleteFlexReservation(token, 4);

            //Assert
            Assert.True(res.StatusCode == System.Net.HttpStatusCode.BadRequest);

        }

        [Fact]
        public async Task DeleteFlexreservation_CancellingTimeBeforeStartingReservation_ReturnOk()
        {
            // Arrange
            var logResponse = await _utility.Login(new LoginModel
            {
                Email = SeededData.User2.Email,
                Password = SeededData.User2Pass,
            });
            string token = await _utility.ExtractToken(logResponse);

            FlexReservationModel model = new FlexReservationModel
            {
                DeskID = 4,
                DtStart = DateTime.Now.AddDays(10),
                DtEnd = DateTime.Now.AddDays(11),
            };

            var createReservationRes = await CreateFlexReservation(token, model);
            string createReservationResponseContent = await _utility.GetContentFromResponse(createReservationRes);

            Reservation reservation = JsonSerializer.Deserialize<Reservation>(createReservationResponseContent);
            //Action
            var res = await DeleteFlexReservation(token, reservation.ReservationID);

            //Assert
            Assert.True(res.StatusCode == System.Net.HttpStatusCode.OK);


        }


        private async Task<HttpResponseMessage> CreateFlexReservation(string token, FlexReservationModel reservationModel)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var serializedReservationModel = JsonSerializer.Serialize(reservationModel);
            StringContent content = new StringContent(serializedReservationModel, Encoding.UTF8, "application/json");
            return await _client.PostAsync(FlexReservationEndPoint, content);

        }


        private async Task<HttpResponseMessage> DeleteFlexReservation(string token, int reservationID)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return await _client.DeleteAsync(FlexReservationEndPoint + "/" + reservationID);
        }

    }
}
