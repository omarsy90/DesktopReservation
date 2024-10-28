using DeskReservationAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeskReservationAPI.Repository
{
    public interface IUserRepository
    {
        Task<Role> GetUserRole(string email, string passowd);  
        Task<User> GetUserByEmail(string email);
        Task<User> AddUser(User user);
        Task<User> GetUser(string email, string password);
        Task<int> GetRoleID(string RoleName);
        Task<User> GetUserByID(string UserID);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public UserRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

      

       

        public async Task<Role> GetUserRole(string email, string passowd)
        {
            User user = await _dbContext.Users.Include(us => us.Role).FirstOrDefaultAsync(us => us.Email == email && us.Password == passowd);
            return user.Role;


        }
        
        public async Task<User>GetUser(string email, string password)
        {
            return await  _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
        public async Task<User> AddUser(User user)
        {
            if(user == null) throw new ArgumentNullException(nameof(user));
             _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<int> GetRoleID(string RoleName)
        {
             Role role = await _dbContext.Roles.FirstOrDefaultAsync(r=>r.Name == RoleName);
            if (role == null) throw new ArgumentNullException(nameof(role));
            return role.RoleID;

        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u=>u.Email == email);
            return user;
        }

    


        public async Task<User> GetUserByID(string userID)
        {
           
            return  await  _dbContext.Users.Include(us=>us.Role).FirstOrDefaultAsync(us=>us.UserID == userID);
        }
      
    }
}
