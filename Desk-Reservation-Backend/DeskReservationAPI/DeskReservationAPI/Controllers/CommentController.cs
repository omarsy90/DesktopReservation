using DeskReservationAPI.Model;
using DeskReservationAPI.Repository;
using DeskReservationAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DeskReservationAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CommentController : Controller
    {
        private ICommentRepository _commentRepository;
        private IAuthenticationService _authService;
        private IDeskRepository _deskRepository;
        public CommentController(ICommentRepository commentRepository,
            IAuthenticationService authentication
            , IDeskRepository deskRepository)
        {
            _commentRepository = commentRepository;
            _authService = authentication;
            _deskRepository = deskRepository;

        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var comments = await _commentRepository.GetComments();
            return Ok(comments);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Index(int id)
        {
            var comment = await _commentRepository.GetCommentByID(id);
            return Ok(comment);
        }

        [HttpPost("")]
        public async Task<IActionResult> Index([FromBody] CommentModel model)
        {
            var user = await _authService.GetUser(this.HttpContext);
            if (user == null)
            {
                return Unauthorized(new { status = PreservedStringMessage.FailedStatus, status_code = 401, message = PreservedStringMessage.UserNotValid });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Desk = await _deskRepository.GetDeskByID(model.DeskID);
            if (Desk == null)
            {
                return NotFound(new {status=PreservedStringMessage.FailedStatus,status_code=404, message= PreservedStringMessage.DeskNotFound });
            }

            Comment comment = new Comment
            {
                UserID = user.UserID.ToString(),
                DeskID = model.DeskID,
                CommentTxt = model.CommentTxt,
                CommentedAt = DateTime.Now,
            };
            var persistedComment = await _commentRepository.Add(comment);

            string serializedComment = Helper.SerializeObject<Comment>(persistedComment);
            return Ok(serializedComment);
        }
    }
}
