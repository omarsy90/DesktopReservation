using Azure.Core;
using DeskReservationAPI.Model;
using DeskReservationAPI.Repository;
using DeskReservationAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using static DeskReservationAPI.Repository.UserRepository;

namespace DeskReservationAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        ITokenManager _tokenGenerator;
        IUserRepository _userRepository;
        private PasswordEncoder _passwordEncoder;
        private EmailTextChecker _emailTextChecker;
        public UserController(ITokenManager tokenGenerator, IUserRepository userRepository)
        {
            _tokenGenerator = tokenGenerator;
            _userRepository = userRepository;
            _passwordEncoder = new PasswordEncoder();
            _emailTextChecker = new EmailTextChecker();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string encodedPass = _passwordEncoder.Encode(model.Password);
         

            if (!await _userRepository.IsUserExist(model.Email,encodedPass )) 
            {
                return Unauthorized(new { status = PreservedStringMessage.FailedStatus, status_code = 401, message = PreservedStringMessage.EmailOrPasswordIncorrectMsg });
            }

            var user = await _userRepository.GetUser(model.Email, encodedPass);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("id", user.UserID.ToString());
            string token = _tokenGenerator.GetToken(dic);
            return Ok(new { status = PreservedStringMessage.SuccessStatus, status_code = 200, token = token });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_emailTextChecker.CheckEmail(model.Email))
            {
                return BadRequest(new { status = PreservedStringMessage.FailedStatus, status_code = 400, message = PreservedStringMessage.EmailTypingInCorrect });
            }

            if (await _userRepository.IsEmailExist(model.Email))
            {
                return BadRequest(new {status= PreservedStringMessage.FailedStatus, status_code =400, message= PreservedStringMessage.EmailExist });
            }

            var user = new User()
            {
                Email = model.Email,
                Password = _passwordEncoder.Encode( model.Password),
                Department = model.Department,
                FirstName = model.FirstName,
                LastName = model.LastName,
                RoleID = await _userRepository.GetRoleID(RoleName.NormalUser),
                UserID = Guid.NewGuid(),

            };

            bool isvalid = await _userRepository.AddUser(user);
            if (!isvalid)
            {
                return Problem(PreservedStringMessage.SeverErrorWhileCreatingUser);
            }
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,

                WriteIndented = false
            };
            var jsonUser = new
            {
                id = user.UserID,
                firstName = model.FirstName,
                lastName = model.LastName,
                department = model.Department,
                email = model.Email,
                password = _passwordEncoder.Encode(model.Password),
                role = RoleName.NormalUser,

            };
            return Ok(new { status = PreservedStringMessage.SuccessStatus, status_code = 201, User = jsonUser });

        }
    }

}
