using System.Text.RegularExpressions;

namespace DeskReservationAPI.Utility
{
    public class EmailTextChecker
    {

        public bool CheckEmail(string email)
        {
            Regex reg = new Regex("[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,64}");
           return  reg.IsMatch(email);
        }
    }
}
