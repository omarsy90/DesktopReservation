using DeskReservationAPI.Model;
using Microsoft.AspNetCore.Http.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeskReservationAPITest
{
    public class Utility
    {


        private static int port = 7070;
         public static string BasicUrl =  $"https://localhost:{port}";

        private HttpClient _client ;
        public Utility(HttpClient httpClient)
        {
           
            _client = httpClient ;
        }
        public async Task<string> GetContentFromResponse(HttpResponseMessage response)
        {
            Stream stream = await response.Content.ReadAsStreamAsync();
            StreamReader reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();
            return json;
        }



        public async Task<string> ExtractToken(HttpResponseMessage response)
        {
            
            string strResponse = await GetContentFromResponse(response);

            if(!strResponse.ToUpper().Contains("token".ToUpper()))
            {
                return string.Empty;
            }
            var substring = strResponse.Split("\"token\":\"");
            var token = substring[1].Split("\"}")[0];
            return token;
        }



        public async Task<HttpResponseMessage> Login(LoginModel loginModel)
        {
            string payload = JsonSerializer.Serialize(new LoginModel { Email = loginModel.Email, Password = loginModel.Password });
            StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{BasicUrl}/User/login", content);

            return response;

        }

        public T Deserialize<T>(string str)
        {
            JsonSerializerOptions option = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

          return   JsonSerializer.Deserialize<T>(str, option);

        }



    }
}
