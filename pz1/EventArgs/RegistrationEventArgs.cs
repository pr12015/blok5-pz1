using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz1.EventArgs
{
    public class RegistrationEventArgs : System.EventArgs
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public RegistrationEventArgs(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
