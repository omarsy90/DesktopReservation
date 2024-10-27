using Microsoft.AspNetCore.Mvc;

namespace DeskReservationAPI.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
