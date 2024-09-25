using DeskReservationAPI.Model;
using DeskReservationAPI.Repository;
using DeskReservationAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DeskReservationAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DeskController : Controller
    {
        private readonly IDeskRepository _deskRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IUserRepository _userRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        public DeskController(IDeskRepository deskRepository
            , ITokenManager tokenManager
            , IUserRepository userRepository
            , IOfficeRepository officeRepository
            , IEquipmentRepository equipmentRepository)
        {
            _deskRepository = deskRepository;
            _tokenManager = tokenManager;
            _userRepository = userRepository;
            _officeRepository = officeRepository;
            _equipmentRepository = equipmentRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var desks = await _deskRepository.GetDesks();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            var desksJson = JsonSerializer.Serialize(desks, options);

            return Ok(desksJson);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var desk = await _deskRepository.GetDeskByID(id);
            if (desk == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            var deskJson = JsonSerializer.Serialize(desk, options);
            return Ok(deskJson);
        }



        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] DeskModel model)
        {

            bool isadmin = await AuthenticateAdmin(HttpContext);
            if (!isadmin)
            {
                return Unauthorized(new { status = PreservedStringMessage.FailedStatus, status_code = 401, message = "Unauthorized" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var office = await RegisterOffice(model.Office);

            var desk = await _deskRepository.AddDesk
                (
                 new Desk { Label = model.Label, Map = model.Map, OfficeID = office.OfficeID, IsDeskActive = model.isActive }
                );


          await  AssignFeuturesToDesk(model.Equipmentst, desk);
            //eager loading to get all data for desk from different tables
            desk = await _deskRepository.GetDeskByID(desk.DeskID);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            var deskJson = JsonSerializer.Serialize(desk, options);

            return Ok(deskJson);
        }

        private async Task<Office>  RegisterOffice(string officeName)
        {
          var office =  await _officeRepository.GetOfficeByName(officeName);
            if (office == null)
            {
                office = await _officeRepository.AddOffice
                    (
                     new Office { Name = officeName }
                    );
            }

            return office;
        }

        private async Task AssignFeuturesToDesk(string equipmentStr, Desk desk)
        {
            string[] equipmentsStringArray = equipmentStr.Split(",");
            foreach (var equipmentString in equipmentsStringArray)
            {
                var equipment = await _equipmentRepository.GetEquipmentByName(equipmentString.Trim());
                if (equipment == null)
                {
                    equipment = await _equipmentRepository.AddEquipment(new Equipment { Feature = equipmentString });
                    await _deskRepository.AddEqipment(desk.DeskID, equipment);
                }
                else
                {
                    await _deskRepository.AddEqipment(desk.DeskID, equipment);
                }
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] DeskModel newModel)
        {
            

            bool isadmin = await AuthenticateAdmin(HttpContext);
            if (!isadmin)
            {
                return Unauthorized(new { status = PreservedStringMessage.FailedStatus, status_code = 401, message = "Unauthorized" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(await _deskRepository.GetDeskByID(newModel.Id) == null)
            {
                return NotFound();
            }


            var office =  await RegisterOffice(newModel.Office);

         var updatedDesk =  await _deskRepository.UpdateDesk(newModel.Id, new Desk
            {
               Label = newModel.Label,
               Map = newModel.Map,
               OfficeID = office.OfficeID,
               IsDeskActive = newModel.isActive
            });

            // update equipments for desk
            AssignFeuturesToDesk(newModel.Equipmentst, await _deskRepository.GetDeskByID(newModel.Id));

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            var deskJson = JsonSerializer.Serialize(updatedDesk, options);

            return Ok(deskJson);

        }


        private async Task<bool> AuthenticateAdmin(HttpContext httpContext)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString();
            string jwtString = token.Substring("Bearer ".Length).Trim();
            string userID = _tokenManager.GetClaimByKey(jwtString, "id");
            if (userID == null)
            {
                return false;
            }

            bool isAdmin = await _userRepository.IsUserAdmin(userID);
            if (!isAdmin)
            {
                return false;
            }
            return true;
        }
    }
}
