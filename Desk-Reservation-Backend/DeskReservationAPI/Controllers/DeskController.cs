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
    [Route("api/[controller]")]
    [Authorize]
    public class DeskController : Controller
    {
        private readonly IDeskRepository _deskRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IAuthenticationService _authService;
        public DeskController(IDeskRepository deskRepository
            , IOfficeRepository officeRepository
            , IEquipmentRepository equipmentRepository
            ,IAuthenticationService authService)
        {
            _deskRepository = deskRepository;
            _officeRepository = officeRepository;
            _equipmentRepository = equipmentRepository;
            _authService = authService;
        }


        /// <summary>
        /// retuens all desks
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// returns desk by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var desk = await _deskRepository.GetDeskByID(id);
            if (desk == null)
            {
                return NotFound(new {status=PreservedStringMessage.FailedStatus, status_code=404 ,message=PreservedStringMessage.DeskNotFound});
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            var deskJson = JsonSerializer.Serialize(desk, options);
            return Ok(deskJson);
        }



        /// <summary>
        /// create Desk.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] DeskModel model)
        {

            bool isadmin = await _authService.AuthenticateAdmin(HttpContext);
            if (!isadmin)
            {
                return Unauthorized(new { status = PreservedStringMessage.FailedStatus, status_code = 401, message = PreservedStringMessage.UserHasNoAdminRole });
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


            await AssignFeuturesToDesk(model.Equipmentst, desk);
            //eager loading to get all data for desk from different tables
            desk = await _deskRepository.GetDeskByID(desk.DeskID);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            var deskJson = JsonSerializer.Serialize(desk, options);

            return Ok(deskJson);
        }

        /// <summary>
        /// update desk.
        /// </summary>
        /// <param name="newModel"></param>
        /// <returns></returns>
        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] DeskModel newModel)
        {
            bool isadmin = await _authService.AuthenticateAdmin(HttpContext);
            if (!isadmin)
            {
                return Unauthorized(new { status = PreservedStringMessage.FailedStatus, status_code = 401, message = PreservedStringMessage.UserHasNoAdminRole });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _deskRepository.GetDeskByID(newModel.Id) == null)
            {
                return NotFound();
            }

            var office = await RegisterOffice(newModel.Office);
            var updatedDesk = await _deskRepository.UpdateDesk(newModel.Id, new Desk
            {
                Label = newModel.Label,
                Map = newModel.Map,
                OfficeID = office.OfficeID,
                IsDeskActive = newModel.isActive
            });

            // update equipments for desk
            AssignFeuturesToDesk(newModel.Equipmentst, await _deskRepository.GetDeskByID(newModel.Id));
            //eager loading to get all data for desk from different tables
            var desk = await _deskRepository.GetDeskByID(updatedDesk.DeskID);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            var deskJson = JsonSerializer.Serialize(desk, options);
            return Ok(deskJson);
        }



     
        private async Task<Office> RegisterOffice(string officeName)
        {
            var office = await _officeRepository.GetOfficeByName(officeName);
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
    }
}
