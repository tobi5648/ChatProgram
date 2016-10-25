using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccess;

namespace Controllers
{
    public class LogInController
    {
        Access a;
        public bool Attempt(out string message, string username, string password)
        {
            message = "";
            bool passwordUsed;
            bool correctPassword;
            a = new Access();
            if (a.CheckForUser(out correctPassword, out passwordUsed, username, password) == true)
            {
                if (passwordUsed == false)
                {
                    message = "A password is required";
                    return false;
                }
                if (correctPassword == false)
                {
                    message = "Wrong password";
                    return false;
                }
                if(correctPassword == true && passwordUsed == true)
                {
                    message = "Logon successfull";
                    return true;
                }
            }
            return false;
        }
    }
}
