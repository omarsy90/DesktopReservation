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
       
        private Utility _utility;

        private int SeededOfficeCout = 5;
        private int SeededEquipmentCout = 2;
        public DeskController_Create_Test(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _utility = new Utility(_client);
        }


        DeskModel newDeskModel = new DeskModel()
        {
            Id = 0
            ,Equipmentst = "socket,screen"
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

            var json = await _utility.GetContentFromResponse(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.True(json.Contains("socket"));
            Assert.True(json.Contains("screen"));
        }

        [Fact]
        public async Task Create_OfficeExist_RetuenDeskWithoutCreatingNewOffice()
        {

            //Arrange
       
            newDeskModel.Office = "office_1";
            //action
            HttpResponseMessage response = await SendCreateDeskRequest(UserType.AdminUser, newDeskModel);

            //Assert
         
            var json = await _utility.GetContentFromResponse(response);
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
            var json = await _utility.GetContentFromResponse(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);

            for ( int i= 0; i<SeededOfficeCout;i++ )
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
            string json = await _utility.GetContentFromResponse(response);
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
            string json = await _utility.GetContentFromResponse(response);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            for (int i = 0; i < SeededEquipmentCout; i++)
            {
                Assert.False(json.Contains($"\"EquipmentID\":{i+1}"));
            }
            
        }




        private async Task<HttpResponseMessage> SendCreateDeskRequest( UserType userType , DeskModel model)
        {
            HttpResponseMessage responseFromLogin;
            if (userType == UserType.AdminUser)
            {
                 responseFromLogin = await _utility.Login( new LoginModel { Email= SeededData.AdminUser.Email , Password= SeededData.AdminUserPassBeforeEncoding} );
            }
            else
            {
                 responseFromLogin = await _utility.Login(new LoginModel { Email = SeededData.User1.Email, Password = SeededData.User1PassBeforEncoding });
            }
            
            string token = await _utility.GetToken(responseFromLogin);
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            string payload = JsonSerializer.Serialize(model);
            StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            //Action
            var response = await _client.PostAsync(Utility.BasicUrl+ "/desk", content);
            return response;
        }



      
       


       



       

      


    }


    public enum UserType
    {
        NormalUser,
        AdminUser
    }
}
