using Microsoft.AspNetCore.Mvc;

namespace DeskReservationAPI.Controllers
{
    public class DeskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
