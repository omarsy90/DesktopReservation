﻿using Azure.Core;
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
        private IAuthenticationService _authService;
        private PasswordEncoder _passwordEncoder;
        private EmailTextChecker _emailTextChecker;
        public UserController( IAuthenticationService authService)
        {
            _passwordEncoder = new PasswordEncoder();
            _emailTextChecker = new EmailTextChecker();
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string encodedPass = _passwordEncoder.Encode(model.Password);
         

            if (!await _authService.IsUserRegistered(model.Email,encodedPass )) 
            {
                return Unauthorized(new { status = PreservedStringMessage.FailedStatus, status_code = 401, message = PreservedStringMessage.EmailOrPasswordIncorrectMsg });
            }

            var user = await _authService.GetUser(model.Email, encodedPass);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("id", user.UserID.ToString());
            string token = _authService.GetToken(dic);
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

            if (await _authService.IsEmailRegistered(model.Email))
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
                RoleID = await _authService.GetRoleID(RoleName.NormalUser),
                UserID = Guid.NewGuid().ToString(),

            };

            User persistedUser = await _authService.AddUser(user);
            if (persistedUser == null)
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