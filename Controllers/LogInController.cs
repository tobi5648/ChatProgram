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
        public bool Attempt(string username, string password)
        {
            a = new Access();
            if (a.CheckForUser())
        }
    }
}
