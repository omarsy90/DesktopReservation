using DeskReservationAPI.Model;
using DeskReservationAPI.Repository;
using DeskReservationAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DeskReservationAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CommentController : Controller
    {
        private ICommentRepository _commentRepository;
        private AuthenticationService _authService;
        public CommentController(ICommentRepository commentRepository , AuthenticationService authentication)
        {
            _commentRepository = commentRepository;
            _authService = authentication;
            
        }

        [HttpGet("")]
        public async  Task<IActionResult> Index()
        {
           var comments =  await _commentRepository.GetComments();
            return Ok(comments);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Index(int commentID)
        {
            var comment = await _commentRepository.GetCommentByID(commentID);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentModel model)
        {
            var user = await _authService.GetUser(this.HttpContext);
            if(user == null)
            {
                return Unauthorized( new {status= PreservedStringMessage.FailedStatus, status_code=401,message="user not valid"});
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Comment comment = new Comment
            {
                UserID = user.UserID.ToString(),
                DeskID = model.DeskID,
                CommentTxt = model.CommentTxt,
                CommentedAt = DateTime.Now,
            };
            var persistedComment = await _commentRepository.Add(comment);
            return Ok(persistedComment);
        }
    }
}
