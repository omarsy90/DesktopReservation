using Azure;
using DeskReservationAPI;
using DeskReservationAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeskReservationAPITest
{
    public class DeskController_Create_Test : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        int port = 7070;

        string BasicUrl;

        private int SeedingOfficeCout = 5;
        private int SeedingEquipmentCout = 2;
        public DeskController_Create_Test(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
            BasicUrl = $"https://localhost:{port}";
        }


        DeskModel newDeskModel = new DeskModel()
        {
            Id = 0
            ,Equipmentst = "socet,screen"
            ,Label = "testdesk"
            ,isActive = true
            ,Map = "test"
            ,Office = "testOffice"
        };

        [Fact]
        public async Task Create_UserUnathorized_ReturnUnAthorized()
        {
            //Arrange
           


            //Action
            var response = await SendCreateDeskRequest(UserType.NormalUser, newDeskModel);

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);
            
        }


        [Fact]
        public async Task Create_PayloadNotConvienent_ReturnBadrequest()
        {
            //Arrange
          
            newDeskModel.Label = string.Empty;
            // Action
            var response = await SendCreateDeskRequest(UserType.AdminUser, newDeskModel);

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            
        }


        [Fact]
        public async Task Create_ValidUserAndValidPayload_ReturnDesk()
        {
            //Arrange
      

            //action
            HttpResponseMessage response = await SendCreateDeskRequest(UserType.AdminUser, newDeskModel);

            //Assert

            var json = await GetContentFromResponse(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.True(json.Contains("socet"));
            Assert.True(json.Contains("screen"));
        }

        [Fact]
        public async Task Create_OfficeExist_RetuenDeskHasWithoutCreatingNewOffice()
        {

            //Arrange
       
            newDeskModel.Office = "office_1";
            //action
            HttpResponseMessage response = await SendCreateDeskRequest(UserType.AdminUser, newDeskModel);

            //Assert
         
            var json = await  GetContentFromResponse(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.True(json.Contains("\"OfficeID\":1"));
           

        }

        [Fact]
        public async Task Create_OfficeNotExist_RetuenDeskAndCreatingNewOffice()
        {
            //Arrange
         
            newDeskModel.Office = "office_100";
            //action
            HttpResponseMessage response = await SendCreateDeskRequest(UserType.AdminUser, newDeskModel);

            //Assert
            var json = await GetContentFromResponse(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);

            for ( int i= 0; i<SeedingOfficeCout;i++ )
            {
                Assert.False(json.Contains($"\"OfficeID\":{i+1}"));
            }
           
        }


        [Fact]
        public async Task Create_EquipemntExist_ReturnDeskWithoutCreatingNewEquipment()
        {

            //Arrange
        
            newDeskModel.Equipmentst = "screen";
            //Action
            HttpResponseMessage response = await SendCreateDeskRequest(UserType.AdminUser, newDeskModel);

            //Assert
            string json = await GetContentFromResponse(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.True(json.Contains("\"EquipmentID\":2"));

        }

        [Fact]
        public async Task Create_EquipemntNotExist_ReturnDeskAndCreatingNewEquipment()
        {
            //Arrange
            newDeskModel.Equipmentst = "headset";
            //Action
            HttpResponseMessage response = await SendCreateDeskRequest(UserType.AdminUser, newDeskModel);
            //Assert
            string json = await GetContentFromResponse(response);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            for (int i = 0; i < SeedingEquipmentCout; i++)
            {
                Assert.False(json.Contains($"\"EquipmentID\":{i+1}"));
            }
            
        }




        private async Task<HttpResponseMessage> SendCreateDeskRequest( UserType userType , DeskModel model)
        {
            HttpResponseMessage responseFromLogin;
            if (userType == UserType.AdminUser)
            {
                 responseFromLogin = await LoginAdmin();
            }
            else
            {
                 responseFromLogin = await LoginNormalUser();
            }
            
            string token = await ExtractToken(responseFromLogin);
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            string payload = JsonSerializer.Serialize(model);
            StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            //Action
            var response = await _client.PostAsync(BasicUrl + "/Desk", content);
            return response;
        }



        private async Task<string> GetContentFromResponse(HttpResponseMessage response)
        {
            Stream stream = await response.Content.ReadAsStreamAsync();
            StreamReader reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();
            return json;
        }

        private async Task<HttpResponseMessage> LoginAdmin()
        {
            string payload = JsonSerializer.Serialize(new LoginModel { Email = "admin@gmail.com", Password = "admin" });
            StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
           HttpResponseMessage response = await _client.PostAsync($"{BasicUrl}/User/login", content);

           return response;

        }


        private async Task<HttpResponseMessage> LoginNormalUser()
        {
            string payload = JsonSerializer.Serialize(new LoginModel { Email = "user@gmail.com", Password = "user" });
            StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{BasicUrl}/User/login", content);

            return response;

        }



        private async  Task< string> ExtractToken(HttpResponseMessage response)
        {
            Stream stream = await response.Content.ReadAsStreamAsync();
            StreamReader reader = new StreamReader(stream);
            string strResponse = reader.ReadToEnd();
            var substring = strResponse.Split("\"token\":\"");
            var token = substring[1].Split("\"}")[0];
            return token;
        }

      


    }


    public enum UserType
    {
        NormalUser,
        AdminUser
    }
}
