using Microsoft.AspNetCore.Mvc;

namespace DeskReservationAPI.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
