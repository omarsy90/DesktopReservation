using DeskReservationAPI;
using DeskReservationAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeskReservationAPITest
{
    public class DeskController_Update_Test : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly Utility _utility;
        private DeskModel newModel;

        private int SeededOfficeCout = 5;
        private int SeededEquipmentCout = 2;

        public DeskController_Update_Test(TestingWebAppFactory<Program> factory)
        {

            _client = factory.CreateClient();
            _utility = new Utility(_client);
           newModel = new DeskModel
            {
                Id = 1,
                Label = "updateDdesk",
                Office = "updatedOffice",
                Equipmentst = "updated eqipments",
                isActive = false,
                Map = "updatedMap"
            };
        }



       


        [Fact]
        public async Task Update_UserUnathorized_ReturnUnAthorized()
        {
            // Arrange

            //Action
            var response = await SendUpdateRequest(UserType.NormalUser, newModel);
            //Asert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);

        }


        [Fact]
        public async Task Update_PayloadNotConvienent_ReturnBadrequest()
        {
            // Arrange
            newModel.Label = string.Empty;
            //Action
            var response = await SendUpdateRequest(UserType.AdminUser, newModel);

            //Asssert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);

        }

        [Fact]
        public async Task Update_DeskNotFound_ReturnNotFound()
        {
            //Arrange
            newModel.Id = 0;

            //Action
            var response = await SendUpdateRequest(UserType.AdminUser, newModel);

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);

        }


        [Fact]
        public async Task Update_OfficeExist_RetuenDeskWithoutCreatingNewOffice()
        {
            //Arrange
            newModel.Office = "oFFicE_2";

            //Action
            var response = await SendUpdateRequest(UserType.AdminUser, newModel);

            //Assert
            string content = await _utility.GetContentFromResponse(response);
            // supposed we have seeded data (already officeID 2 exist) 
            Assert.True(content.ToUpper().Contains("\"OfficeID\":2".ToUpper())); 

        }


        public async Task Update_OfficeNotExist_RetuenDeskAndCreatingNewOffice()
        {
            //Arrange
            newModel.Office = "office_6";  //supposed it does not exist in seeded data

            //Action
                var response = await SendUpdateRequest(UserType.AdminUser, newModel);

            //Aseert
            string content = await _utility.GetContentFromResponse(response);

            for(int i=0; i<SeededOfficeCout; i++)
            {
                Assert.False(content.ToUpper().Contains($"\"OfficeID\":{i}"));
            }
          
        }


        [Fact]
        public async Task Update_EquipemntExist_ReturnDeskWithoutCreatingNewEquipment()
        {
            //Arrange
            newModel.Equipmentst = "screen,socket"; // supposed the both exist in seeded data
            //Action
            var response = await SendUpdateRequest(UserType.AdminUser, newModel);

            //Assert
             string content = await _utility.GetContentFromResponse(response);
            Assert.True(content.ToUpper().Contains($"\"{nameof(Equipment.EquipmentID)}\":1".ToUpper()));
            Assert.True(content.ToUpper().Contains($"\"{nameof(Equipment.EquipmentID)}\":2".ToUpper()));
        }


        [Fact]
        public async Task Update_EquipemntNotExist_ReturnDeskAndCreatingNewEquipment()
        {
            //Arrange
            newModel.Equipmentst = "big screen , headset"; // supposed the both exist in seeded data
            //Action
            var response = await SendUpdateRequest(UserType.AdminUser, newModel);

            //Assert
            string content = await _utility.GetContentFromResponse(response);
            for(int i=0;i <SeededEquipmentCout; i++)
            {
                Assert.False(content.ToUpper().Contains($"\"{nameof(Equipment.EquipmentID)}\":{i}".ToUpper()));
            }
           

        }


       private async Task<HttpResponseMessage> SendUpdateRequest(UserType userType , DeskModel newModel)
        {

            HttpResponseMessage responseFromLogin;
            if (userType == UserType.AdminUser)
            {
                responseFromLogin = await _utility.Login(new LoginModel { Email = "admin@gmail.com", Password = "admin" });
            }
            else
            {
                responseFromLogin = await _utility.Login(new LoginModel { Email = "user@gmail.com", Password = "user" });
            }

            string token = await _utility.ExtractToken(responseFromLogin);
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            string payload = JsonSerializer.Serialize(newModel);
            StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            //Action
            var response = await _client.PutAsync(Utility.BasicUrl + "/Desk", content);
            return response;

        }


    }

    
}
