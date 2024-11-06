using DeskReservationAPI;
using DeskReservationAPI.Model;
using DeskReservationAPI.Utility;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace DeskReservationAPITest
{
    public class UserControllerTest : IClassFixture<TestingWebAppFactory<Program>>
    {

        private readonly HttpClient _client;
        int port = 7070;

        string endpoint = Utility.BasicUrl+"/User";

        public UserControllerTest(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
            endpoint = $"https://localhost:{port}/User";
        }


        [Fact]
        public async Task Login_UnvalidPayload_RetuenBadRequest()
        {
            //Arrange 
            LoginModel model = new LoginModel() { Email = string.Empty, Password = string.Empty };

            //Action
            var response = await SendLoginRequest(model);
            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);

        }


        [Fact]

        public async Task Login_UnvalidCredential_ReturnUnauthorized()
        {
            //Arrange
            LoginModel model = new LoginModel
            {
                Email = "test@gmail.com",
                Password = "1234",
            };


            //Action
            var response = await SendLoginRequest(model);
            //Assert
            string content = await ReadResponseContent(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);
            Assert.True(content.Contains(PreservedStringMessage.EmailOrPasswordIncorrectMsg));
        }


        [Fact]
        public async Task Login_ValidCredential_RetrunToken()
        {
            //Arrange
            var registerModel = new RegisterModel
            {
                FirstName = "string",
                LastName = "string",
                Email = "name@email.com",
                UserName = "string",
                Password = "string",
                Department = "string",

            };

            await CreateFakeUserInDB(registerModel);
            LoginModel model = new LoginModel()
            {
                Email = registerModel.Email,
                Password = registerModel.Password,
            };

            //Action
            var response = await SendLoginRequest(model);
            //Assert
            string content = await ReadResponseContent(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.True(content.ToUpper().Contains("token".ToUpper()));

        }


        [Fact]
        public async Task Register_UnvalidEmailTyping_ReturnBadReqest()
        {
            //Arrange
            var registerModel = new RegisterModel
            {
                Email = " email ",
                Password = "t",
                Department = "t ",
                FirstName = " t",
                LastName = " t",
                UserName = " t"


            };



            //Action
            var response = await SendRegisterRequest(registerModel);


            //Assert
            string content = await ReadResponseContent(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.True(content.Contains(PreservedStringMessage.EmailTypingInCorrect));

        }

        private async Task<string> ReadResponseContent(HttpResponseMessage response)
        {
            Stream stream = await response.Content.ReadAsStreamAsync();
            StreamReader reader = new StreamReader(stream);
            string stringContent = reader.ReadToEnd();
            return stringContent;
        }

        [Fact]
        public async Task Register_EmailAlreadyExist_ReturnBadRequest()
        {
            //Arange
            var registerModel = new RegisterModel
            {
                FirstName = "string",
                LastName = "string",
                Email = "name@email.com",
                UserName = "string",
                Password = "string",
                Department = "string",

            };

            await CreateFakeUserInDB(registerModel);

            //Action

            var response = await SendRegisterRequest(registerModel);

            //Assert
            string resMssage = await ReadResponseContent(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.True(resMssage.Contains(PreservedStringMessage.EmailExist));
        }



        [Fact]
        public async Task Register_ValidPayload_ReturnOK()
        {
            // Arrange

            var registerModel = new RegisterModel
            {
                FirstName = "string",
                LastName = "string",
                Email = "name2@email.com",
                UserName = "string",
                Password = "string",
                Department = "string",

            };


            // Action
            var response = await SendRegisterRequest(registerModel);


            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
        }



        public async Task<HttpResponseMessage> SendLoginRequest(LoginModel model)
        {
            string jsonStr = JsonSerializer.Serialize(model);
            StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{endpoint}/login", content);
            return response;
        }


        public async Task<HttpResponseMessage> SendRegisterRequest(RegisterModel model)
        {
            string jsonStr = JsonSerializer.Serialize(model);
            StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{endpoint}/register", content);
            return response;
        }


        private async Task CreateFakeUserInDB(RegisterModel registerModel)
        {
            string jsonStr = JsonSerializer.Serialize(registerModel);
            StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{endpoint}/register", content);
        }

    }
}