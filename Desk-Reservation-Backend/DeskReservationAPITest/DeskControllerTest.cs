using DeskReservationAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskReservationAPI.Model;
namespace DeskReservationAPITest
{
    public class DeskControllerTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        HttpClient _client;
        private Utility _utility;
        public DeskControllerTest(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _utility = new Utility(_client);
        }



        [Fact]
        public async Task Index_UnvalidUserToken_ReturnUnauthorized()
        {
            //Arrange



            //Action
            var respone = await GetAllDesk("unvalidToken");

            //Assert
            string content = await _utility.GetContentFromResponse(respone);
            Assert.True(respone.StatusCode == System.Net.HttpStatusCode.Unauthorized);

        }


        [Fact]
        public async Task Index_ValidUserToken_ReturnListOfDesk()
        {
            //Arrange
            var loginResponse = await _utility.Login(new LoginModel { Email = "user@gmail.com", Password = "user" });
            string token = await _utility.GetToken(loginResponse);
            //Action
            var response = await GetAllDesk(token);
            //Assert
            string content = await _utility.GetContentFromResponse(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.True(content.ToUpper().Contains("Desk".ToUpper()));

        }



        [Fact]
        public async Task Get_UnValidUserToken_ReturnUnauthorized()
        {
            //Arrange

            string token = "unvalidToken";
            int deskId = 1; // supposed this ID seeded in database
            //Action
             var response = await GetDeskByID(token, deskId);

            //Assert
             Assert.True(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);
        }



        [Fact]
        public async Task Get_ValidUserToken_ReturnCorrespondingDesk()
        {

            //Arrange
           var loginResponse = await _utility.Login(new LoginModel { Email="user@gmail.com",Password= "user" });
            string token = await _utility.GetToken(loginResponse);
            int deskID = 1; // supposed this ID  seeded in database

            //Action
            var response = await GetDeskByID(token, deskID);
            //Assert
            string content = await _utility.GetContentFromResponse(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.True(content.ToUpper().Contains("desk".ToUpper()));

        }

        [Fact]
        public async Task Get_IdNotExist_ReturnNotFound()
        {

            //Arrange
            var loginResponse = await _utility.Login(new LoginModel { Email = "user@gmail.com", Password = "user" });
            string token = await _utility.GetToken(loginResponse);
            int deskID = -1; // supposed this ID  seeded in database

            //Action
            var response = await GetDeskByID(token, deskID);
            //Assert
         
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);
    

        }


        private async Task<HttpResponseMessage> GetAllDesk(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var respone = await _client.GetAsync(Utility.BasicUrl + "/Desk");
            return respone;
        }


        private async Task<HttpResponseMessage> GetDeskByID( string token,int id)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var respone = await _client.GetAsync(Utility.BasicUrl + "/Desk/"+id);
            return respone;
        }

    }
}
