using DeskReservationAPI.Model;
using DeskReservationAPI.Repository;

namespace DeskReservationAPI.Utility
{
    public class AuthenticationService
    {

        private readonly IUserRepository _userRepository;
        private readonly ITokenManager _tokenManager;
        public AuthenticationService(IUserRepository userRepository , ITokenManager tokenManager)
        {
            _userRepository = userRepository;
            _tokenManager = tokenManager;
        }

        public async Task<bool> AuthenticateAdmin(HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["Authorization"].ToString();
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



      
        public async Task<User> GetUser(HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["Authorization"].ToString();
            string jwtString = token.Substring("Bearer".Length).Trim();
            string userID = _tokenManager.GetClaimByKey(jwtString, "id");
            var user =  await _userRepository.GetUserByID(userID);
            return user;
        }


    }
}
