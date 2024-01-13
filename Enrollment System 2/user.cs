using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment_System_2
{
    class user
    {
        public int id { get; set; }
        public string Username { get; set; }

        public user(int userId, string username)
        {
            id = userId;
            Username = username;
        }

        public static class CurrentUser
        {
            public static user User { get; set; }
        }
    }
}
