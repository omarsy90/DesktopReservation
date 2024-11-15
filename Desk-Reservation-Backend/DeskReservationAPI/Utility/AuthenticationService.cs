using DeskReservationAPI.Model;
using DeskReservationAPI.Repository;
using System.Text;

namespace DeskReservationAPI.Utility
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAdmin(HttpContext httpContext);
        Task<User> GetUser(HttpContext httpContext);
        Task<User> GetUserByID(string userID);
        Task<User> UpdateUser(string userID, UserModel model);
        Task<bool> IsUserRegistered(string email, string pass);
        Task<User> GetUser(string email, string pass);
        Task<bool> IsEmailRegistered(string email);
        Task<User> AddUser(User user);
        Task<int> GetRoleID(string RoleName);
         string GetToken(Dictionary<string, string> dic);
        Task<Role> GetUserRole(string email, string pass);
    }

    public class AuthenticationService : IAuthenticationService
    {

        private readonly IUserRepository _userRepository;
        private readonly TokenManager _tokenManager;
        public AuthenticationService(IUserRepository userRepository, TokenManager tokenManager)
        {
            _userRepository = userRepository;
            _tokenManager = tokenManager;
        }


        public string GetToken(Dictionary<string,string> dic)
        {
           return   _tokenManager.GetToken(dic);
        }

        public async Task<int> GetRoleID(string RoleName)
        {
         return await    _userRepository.GetRoleID(RoleName);
        }

        public async Task<bool> IsEmailRegistered(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return false;
            }
            return true;
        }
        public async Task<User> AddUser(User user)
        {
            return await _userRepository.AddUser(user);
        }

        public async Task<User> GetUser(string email, string pass)
        {
            return await _userRepository.GetUser(email, pass);
        }

        public async Task<Role> GetUserRole(string email, string pass)
        {
            return await _userRepository.GetUserRole(email, pass);
        }
        public async Task<bool> IsUserAdmin(string email, string pass)
        {
            var role = await _userRepository.GetUserRole(email, pass);
            if (role.Name == RoleName.Admin)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsUserRegistered(string email, string pass)
        {
            var user = await _userRepository.GetUser(email, pass);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AuthenticateAdmin(HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["Authorization"].ToString();
            string jwtString = token.Substring("Bearer ".Length).Trim();
            string userID = _tokenManager.GetClaimByKey(jwtString, "id");
            if (string.IsNullOrEmpty(userID))
            {
                return false;
            }
          var user = await _userRepository.GetUserByID(userID);
        
            if (user.Role.Name == RoleName.Admin)
            {
                return true;
            }
            return false;
        }

        public async Task<User> GetUser(HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["Authorization"].ToString();
            string jwtString = token.Substring("Bearer ".Length).Trim();
            string userID = _tokenManager.GetClaimByKey(jwtString, "id");
            var user = await _userRepository.GetUserByID(userID);
            return user;
        }

        public async Task<User> GetUserByID(string userID)
        {
            var user = await _userRepository.GetUserByID(userID);
            return user;
        }

        public async Task<User> UpdateUser( string userID ,UserModel model )
        {
          PasswordEncoder encoder = new PasswordEncoder();
            var newUser = new User
            {
                UserID = userID,
                Email = model.Email,
                Password =  encoder.Encode( model.Password),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Department = model.Department,

            };

           var user = await _userRepository.UpdateUser(newUser);
            return user;
        }
    }

    public class RoleName
    {
        public static string NormalUser = "user";
        public static string Admin = "admin";
    }
}
