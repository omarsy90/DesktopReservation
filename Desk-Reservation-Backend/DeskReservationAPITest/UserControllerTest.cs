using DeskReservationAPI;
using DeskReservationAPI.Model;
using DeskReservationAPI.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json.Linq;
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
        private Utility _utility;

        public UserControllerTest(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _utility = new Utility(_client);
        
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



      

        [Fact]

        public async Task UpdateUser_validPayload_ReturnUserWithNewInfo()
        {
            //Arrange
           var logResponse = await SendLoginRequest(new LoginModel
            {
                Email = SeededData.User3.Email,
                Password = SeededData.User3PassBeforeEncoding,
            });
            var token = await _utility.GetToken(logResponse);

            var userModel = GetFakeUserModel();
            //Action     
            var response  = await UpdateUser(token, userModel);
            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            string responseContent = await _utility.GetContentFromResponse(response);
           PasswordEncoder encoder = new PasswordEncoder();
            var user = _utility.Deserialize<User>(responseContent);
            Assert.NotNull(user);
            Assert.True(user.Email == userModel.Email);
            Assert.True(user.Password == encoder.Encode(userModel.Password));
            Assert.True(user.Department == userModel.Department);
            Assert.True(user.FirstName == userModel.FirstName);
            Assert.True(user.LastName == userModel.LastName);

        }


        [Fact]
        public async Task UpdateUser_TokenNotValid_ReturnUnAuthorized()
        {
            //Arrange
            var userModel = GetFakeUserModel();
            string token = "unvalid";

            //Action
            var response = await UpdateUser(token, userModel);

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);

        }


        [Fact]
        public async Task UpdateUser_UserModelNotValid_ReturnBadRequest()
        {
            //Arrange
            var logResponse = await SendLoginRequest(new LoginModel
            {
                Email = SeededData.User3.Email,
                Password = SeededData.User3PassBeforeEncoding,
            });
            var token =   await _utility.GetToken(logResponse);

            var userModel = GetFakeUserModel();
            userModel.Email = string.Empty;
            //Action
            var response = await UpdateUser(token, userModel);
            //Assert
            string str =  await _utility.GetContentFromResponse(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);

        }

        [Fact]
        public async Task GetUser_ValidUserID_ReturnUser()
        {
            // Arrange
            var logResponse = await SendLoginRequest(new LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1PassBeforEncoding,
            });
            var token = await _utility.GetToken(logResponse);

            //Action
            var response = await GetUser(token, SeededData.User1.UserID);

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            string resContent = await _utility.GetContentFromResponse(response);
            var user = JsonSerializer.Deserialize<User>(resContent);
            Assert.True(user.UserID == SeededData.User1.UserID);
            Assert.True(isPasswordHidden(user.Password,'*'));

        }



        [Fact]
        public async Task GetUser_UnvalidUserID_ReturnNotFound()
        {
            // Arrange
            var logResponse = await SendLoginRequest(new LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1PassBeforEncoding,
            });
            var token = await _utility.GetToken(logResponse);

            //Action
            var response = await GetUser(token, "No Found User ID");

            //Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);

        }



        private UserModel GetFakeUserModel()
        {
            UserModel userModel = new UserModel
            {
                Email = "newEmail@gmail.com",
                Password = "new Password",
                Department = "new Department",
                FirstName = "new firstName",
                LastName = " new lastName",
            };

            return userModel;
        }


        private bool isPasswordHidden(string password , char hidingChar)
        {
            foreach (char ch in password)
            {
                if(ch != hidingChar)
                {
                    return false;
                }
            }

            return true;
        }

        private async Task<HttpResponseMessage> GetUser(string token , string userID)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{endpoint}/{userID}");
            return response;

        }

        public async Task<HttpResponseMessage> UpdateUser(string token , UserModel model)
        {

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            string jsonStr = JsonSerializer.Serialize(model);
            StringContent content = new StringContent(jsonStr,Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{endpoint}", content);
            return response;
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