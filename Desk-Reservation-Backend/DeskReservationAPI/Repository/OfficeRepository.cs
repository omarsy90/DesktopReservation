using DeskReservationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DeskReservationAPI.Repository
{
    public interface IOfficeRepository
    {
        Task<Office> GetOfficeByName(string officeName);
        Task<Office> AddOffice(Office office);
    }

    public class OfficeRepository : IOfficeRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public OfficeRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Office> AddOffice(Office office)
        {
              await _dbContext.Offices.AddAsync(office);
               await _dbContext.SaveChangesAsync();
            return office;
        }

        public async Task<Office> GetOfficeByName(string officeName)
        {
            return await _dbContext.Offices.FirstOrDefaultAsync(of => of.Name.ToUpper() == officeName.ToUpper());
        }


    }
}
