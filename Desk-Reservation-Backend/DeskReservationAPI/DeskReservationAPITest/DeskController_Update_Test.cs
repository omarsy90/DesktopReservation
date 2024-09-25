using DeskReservationAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskReservationAPITest
{
    public class DeskController_Update_Test : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        int port = 7070;

        string BasicUrl;

        public DeskController_Update_Test(TestingWebAppFactory<Program> factory)
        {

            _client = factory.CreateClient();
            BasicUrl = $"https://localhost:{port}/Desk";


        }


        public async Task Update_UserUnathorized_ReturnUnAthorized()
        {

        }

        public async Task Update_PayloadNotConvienent_ReturnBadrequest()
        {

        }

        public async Task Update_DeskNotFound_ReturnNotFound()
        {

        }

        public async Task Update_OfficeExist_RetuenDeskWithoutCreatingNewOffice()
        {

        }

        public async Task Update_OfficeNotExist_RetuenDeskAndCreatingNewOffice()
        {

        }

        public async Task Update_EquipemntExist_ReturnDeskWithoutCreatingNewEquipment()
        {

        }

        public async Task Update_EquipemntNotExist_ReturnDeskAndCreatingNewEquipment()
        {

        }




    }
}
