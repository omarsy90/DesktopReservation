using DeskReservationAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DeskReservationAPI.Model;
using System.Text.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Primitives;
using DeskReservationAPI.Utility;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
namespace DeskReservationAPITest
{
    public class FixReservationControllerTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        HttpClient _client;
        Utility _utility;
        PasswordEncoder _encoder;
        string FixReservationEndPoint = Utility.BasicUrl + "/FixReservation";
        public FixReservationControllerTest(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _utility = new Utility(_client);
            _encoder = new PasswordEncoder();
        }


        [Fact]
        public async Task CreateFixReservationRequest_NoOverlappedReservation_ReturnOk()
        {

            //Arrange
            var res = await _utility.Login(new LoginModel
            { Email = SeededData.User1.Email, Password = SeededData.User1Pass }
            );

            string token = await _utility.ExtractToken(res);


            ReservationModel model = new ReservationModel()
            {
                DeskID = 5,
                DtStart = DateTime.Parse("2025-03-01"),
                isFavourited = true,
            };

            //Action
            var response = await SendFixReservationRequest(model, token);

            //Assert

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);




        }


        [Fact]
        public async Task CreateFixReservationRequest_ThereIsOverlappedReservation_ReturnBadRequest()
        {
            //Arrange
            var res = await _utility.Login(new LoginModel
            { Email = SeededData.User2.Email, Password = SeededData.User2Pass }
            );

            string token = await _utility.ExtractToken(res);


            ReservationModel model = new ReservationModel()
            {
                DeskID = 1,
                DtStart = DateTime.Parse("2024-11-28"),
                isFavourited = true,
            };

            //Action
            var response = await SendFixReservationRequest(model, token);

            //Assert
            string resContent = await _utility.GetContentFromResponse(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.True(resContent.Contains(PreservedStringMessage.DeskAlreadyReserved));

        }


        [Fact]
        public async Task CreateFixReservationRequest_UserHasAlreadyReservationInDurationRequested_ReturnBadRequest()
        {
            //Arrange
            var loginReseponse = await _utility.Login(new LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1Pass
            });
            string token = await _utility.ExtractToken(loginReseponse);
            ReservationModel model = new ReservationModel()
            {
                DeskID = 5,
                DtStart = DateTime.Parse("2024-12-29"),
                isFavourited = true
            };
            //Action
            var response = await SendFixReservationRequest(model, token);

            //Assert
            string resContent = await _utility.GetContentFromResponse(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.True(resContent.Contains(PreservedStringMessage.UserHasAlreadyReservationInTimeRequested));


        }

        [Fact]
        public async Task ConfirmFixReservation_UserIsAdminAndValidReservationID_ReturnOk()
        {

            //Arrange
            var res = await _utility.Login(new LoginModel
            { Email = SeededData.AdminUser.Email, Password = SeededData.AdminUserPass }
            );

            string token = await _utility.ExtractToken(res);


            //Action
            var response = await ConfirmFixReservation(3, false, token);

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            string resContent = await _utility.GetContentFromResponse(response);

            Assert.True(resContent.ToUpper().Contains(nameof(Reservation.ReservationID).ToUpper()));



        }



        [Fact]
        public async Task ConfirmFixReservation_DeskIsBookedInThePeriodRequested_ReturnBadequest()
        {

            //Arrange
            var res = await _utility.Login(new LoginModel
            { Email = SeededData.AdminUser.Email, Password = SeededData.AdminUserPass }
            );

            string token = await _utility.ExtractToken(res);


            //Action
            var response = await ConfirmFixReservation(7, true, token);

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);

        }





        [Fact]
        public async Task ConfirmFixReservation_UserIsNotAdmin_ReturnUnAuthorized()
        {
            //Arrange
            var loginRes = await _utility.Login(new LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1Pass
            });
            string token = await _utility.ExtractToken(loginRes);

            //Action
            var response = await ConfirmFixReservation(3, false, token);


            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);

        }

        [Fact]
        public async Task ConfirmFixReservation_ReservationIDNotValid_ReturnNotFound()
        {
            //Arrange
            var loginRes = await _utility.Login(new LoginModel
            {
                Email = SeededData.AdminUser.Email,
                Password = SeededData.AdminUserPass
            });
            string token = await _utility.ExtractToken(loginRes);

            //Action
            var response = await ConfirmFixReservation(-1, false, token);

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);

        }

        [Fact]
        public async Task GetFixReservationRequestsByUser_UnvalidUser_ReturnUnahtorized()
        {
            //Arrange
            var loginRes = await _utility.Login(new LoginModel
            {
                Email = "unvalid@gmail.com",
                Password = "unvalidPass"
            });

            string token = await _utility.ExtractToken(loginRes);

            //Action
            var response = await GetFixReservationRequestsForUser(token);

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);

        }

        [Fact]
        public async Task GetFixReservationRequestsByUser_UserAvailable_ReturnOK()
        {
            //Arrange

            var loginRes = await _utility.Login(new LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1Pass
            });

            string token = await _utility.ExtractToken(loginRes);
            //Action
            var response = await GetFixReservationRequestsForUser(token);

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            string resContent = await _utility.GetContentFromResponse(response);

            var fixReservations = _utility.Deserialize<IEnumerable<FixReservation>>(resContent);

            Assert.True(fixReservations.ToList().Count > 0);

        }


        [Fact]
        public async Task GetFixReservationConfirmed_UnvalidUser_ReturnUnauthorized()
        {
            //Arrange
            var loginRes = await _utility.Login(new LoginModel
            {
                Email = "unvalid@gmail.com",
                Password = "unvalidpass"
            });

            string token = await _utility.ExtractToken(loginRes);
            //Action
            var response = await GetFixReservationConfirmedForUser(token);
            //Assert
             Assert.True(response.StatusCode  == System.Net.HttpStatusCode.Unauthorized);
        }


        [Fact]
        public async Task GetFixReservationConfirmed_UserAvailable_ReturnOK()
        {
            //Arrange
            var loginRes = await _utility.Login(new LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1Pass
            });

            string token = await _utility.ExtractToken(loginRes);
            //Action
            var response = await GetFixReservationConfirmedForUser(token);

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            string resContent = await _utility.GetContentFromResponse(response);
             var fixReservations = _utility.Deserialize<IEnumerable<FixReservation>>(resContent);
            Assert.True(fixReservations.ToList().Any());   


        }




        [Fact]
        public async Task GetReservationByID_IdReservationUnvalid_ReturnNotFound()
        {
            // Arrange
            var loginRes = await _utility.Login(new LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1Pass
            });
            string token = await _utility.ExtractToken(loginRes);
            //Action
            var response = await GetReservationByID(token, -1);

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);

        }

        [Fact]

        public async Task GetReservationByID_IdReservationValid_ReturnOK()
        {
            // Arrange
            var loginRes = await _utility.Login(new LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1Pass
            });
            string token = await _utility.ExtractToken(loginRes);
            //Action
            var response = await GetReservationByID(token, 1);

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            string strContent =  await _utility.GetContentFromResponse(response);
            var reservation = _utility.Deserialize<FixReservation>(strContent);
            Assert.True(reservation.ReservationID == 1);

        }


        [Fact]
        public async Task GetAllFixReservationRequests_UserNotAdmin_ReturnUnauthorized()
        {
            //Arrange

            var loginRes = await _utility.Login(new LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1Pass
            });
            string token = await _utility.ExtractToken(loginRes);

            //Action
               var response = await   GetAllFixReservationRequests(token);

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);

        }

        [Fact]
        public async Task GetAllFixReservationRequests_UserisAdmin_ReturnOk()
        {
            //Arrange

            var loginRes = await _utility.Login(new LoginModel
            {
                Email = SeededData.AdminUser.Email,
                Password = SeededData.AdminUserPass
            });
            string token = await _utility.ExtractToken(loginRes);

            //Action
            var response = await GetAllFixReservationRequests(token);

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            string content = await _utility.GetContentFromResponse(response);
            var reservations = _utility.Deserialize<IEnumerable<FixReservation>>(content);

            Assert.True(reservations.Count() > 0);
        }




        private async Task<HttpResponseMessage> SendFixReservationRequest(ReservationModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonSerializer.Serialize(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(FixReservationEndPoint+ "/request", content);
            return response;
        }

        private async Task<HttpResponseMessage> ConfirmFixReservation(int reservationID, bool isConfirmed, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonSerializer.Serialize(new { isConfirmed });
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(FixReservationEndPoint + "/request/" + reservationID, content);
            return response;
        }



        private async Task<HttpResponseMessage> GetFixReservationRequestsForUser(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(FixReservationEndPoint+ "/request");
            return response;

        }

        public async Task<HttpResponseMessage> GetFixReservationConfirmedForUser(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(FixReservationEndPoint + "/confirmed");
            return response;

        }

        public async Task<HttpResponseMessage> GetReservationByID(string token,int reservationID)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(FixReservationEndPoint + $"/{reservationID}");
            return response;
        }

        public async Task<HttpResponseMessage> GetAllFixReservationRequests(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(FixReservationEndPoint+ "/request/all");
            return response;
        }

    }
}
