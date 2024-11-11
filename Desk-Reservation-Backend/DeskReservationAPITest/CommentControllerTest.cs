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
    public class CommentControllerTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        HttpClient _client;
        string endPoint = Utility.BasicUrl + "/Comment";
        Utility _utility;

        public CommentControllerTest( TestingWebAppFactory<Program> factory)
        {
                _client = factory.CreateClient();
               _utility = new Utility( _client );

        }


        [Fact]
        public async Task Index_ValidRequest_ReturnOk()
        {
            //Arrange
           var logResponse= await _utility.Login(new DeskReservationAPI.Model.LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1Pass
            });

            string token = await _utility.ExtractToken( logResponse );
            //Action
            var resposne = await   GetComments( token );

            //Assert
             string responseContent = await _utility.GetContentFromResponse( resposne );
            Assert.True(resposne.StatusCode == System.Net.HttpStatusCode.OK );
            Assert.True(!string.IsNullOrEmpty( responseContent ) ); // it depends on seeded data  
        }



        [Fact]
        /// index
        public async Task GetCommentByID_ValidRequest_ReturnOk()
        {
            //Arrange
            var logResponse = await _utility.Login(new DeskReservationAPI.Model.LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1Pass
            });

            string token = await _utility.ExtractToken(logResponse);
            //Action
            var resposne = await GetComment(token,1);

            //Assert
            string responseContent = await _utility.GetContentFromResponse(resposne);
            Assert.True(resposne.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.True(!string.IsNullOrEmpty(responseContent)); // it depends on seeded data  
        }



        [Fact]
        /// <summary>
        ///  post index
        /// </summary>
        /// <returns></returns>
        public async Task  CreateComment_validPayload_ReturnOK()
        {
            //Arrange
            var logResponse = await _utility.Login(new DeskReservationAPI.Model.LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1Pass
            });
            string token = await _utility.ExtractToken(logResponse);

            CommentModel model = new CommentModel { CommentTxt = "test", DeskID = 5 };
            //Action
            var response = await    CreateComment(token, model);

            //Assert
             string responseContent = await _utility.GetContentFromResponse(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.True(!string.IsNullOrEmpty(responseContent)); // it depends on seeded data 

        }


        [Fact]
        public async Task CreateComment_UnValidUser_ReturnUnAuthorized()
        {
            //Arrange
            

            CommentModel model = new CommentModel { CommentTxt = "test", DeskID = 5 };
            //Action
            var response = await CreateComment("fake token", model);

            //Assert
            string responseContent = await _utility.GetContentFromResponse(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);
           

        }


        [Fact]
        public async Task CreateComment_CommentTextIsEmpty_ReturnBadRequest()
        {
            //Arrange
            var logResponse = await _utility.Login(new DeskReservationAPI.Model.LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1Pass
            });
            string token = await _utility.ExtractToken(logResponse);

            CommentModel model = new CommentModel { CommentTxt = "" , DeskID=1 };
            //Action
            var response = await CreateComment(token, model);

            //Assert
            string responseContent = await _utility.GetContentFromResponse(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
         
        }

        [Fact]
        public async Task CreateComment_DeskNotExist_ReturnNotFound()
        {
            //Arrange
            var logResponse = await _utility.Login(new DeskReservationAPI.Model.LoginModel
            {
                Email = SeededData.User1.Email,
                Password = SeededData.User1Pass
            });
            string token = await _utility.ExtractToken(logResponse);

            CommentModel model = new CommentModel { CommentTxt = "test", DeskID = int.MaxValue }; // it is supposed that no desk whose id equals int.MaxValue
            //Action
            var response = await CreateComment(token, model);

            //Assert
        
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);


        }





        private async Task<HttpResponseMessage> GetComments(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
          var response = await  _client.GetAsync(endPoint);
            return response;
        }

        private async Task<HttpResponseMessage> GetComment(string token , int id )
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(endPoint+$"/{id}");
            return response;
        }

        private async Task<HttpResponseMessage> CreateComment(string token,CommentModel model)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var jsonStr = JsonSerializer.Serialize(model);
            StringContent content = new StringContent(jsonStr,Encoding.UTF8,"application/json");
            var response = await _client.PostAsync(endPoint,content);
            return response;

        }


    }
}
