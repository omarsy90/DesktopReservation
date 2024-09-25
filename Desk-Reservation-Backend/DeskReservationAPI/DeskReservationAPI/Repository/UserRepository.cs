using DeskReservationAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeskReservationAPI.Repository
{
    public interface IUserRepository
    {
        Task<Role> GetUserRole(string email, string passowd);
        Task<bool> IsUserAdmin(string email, string password);
        Task<bool> IsUserExist(string email, string password);
        Task<bool> IsEmailExist(string email);
        Task<bool> AddUser(User user);
        Task<User> GetUser(string email, string password);
        Task<int> GetRoleID(string RoleName);
        Task<bool> IsUserAdmin(string userID);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public UserRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<bool> IsUserExist(string email, string password)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(us => us.Email == email && us.Password == password);
            return user != null;
        }

        public async Task<bool> IsUserAdmin(string email, string password)
        {
            User user = await _dbContext.Users.Include(us => us.Role).FirstOrDefaultAsync(us => us.Email == email && us.Password == password && us.Role.Name == RoleName.Admin);
            return user != null;
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
        public async Task<bool> AddUser(User user)
        {
            if(user == null) throw new ArgumentNullException(nameof(user));
             _dbContext.Add(user);
           int state =   await _dbContext.SaveChangesAsync();
             return state > 0;
        }

        public async Task<int> GetRoleID(string RoleName)
        {
             Role role = await _dbContext.Roles.FirstOrDefaultAsync(r=>r.Name == RoleName);
            if (role == null) throw new ArgumentNullException(nameof(role));
            return role.RoleID;

        }

        public async Task<bool> IsEmailExist(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u=>u.Email == email);
            return user != null;
        }

        public async Task<bool> IsUserAdmin(string userID)
        {
            var user = await _dbContext.Users.Include(u=>u.Role).FirstOrDefaultAsync(us => us.UserID.ToString() == userID);
            if(user == null)  return false;

           return  user.Role.Name == RoleName.Admin;
            
        }

        public class RoleName
        {
            public static string NormalUser = "user";
            public static string Admin = "admin";
        }
    }
}
