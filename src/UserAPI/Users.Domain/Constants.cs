using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Domain
{
    internal class Constants
    {
        public static class Roles
        {
            public const string Admin = "admin";
            public const string User = "user";

            internal static bool IsValidRole(string role)
            {
                return role == Roles.Admin || role == Roles.User;
            }
        }
    }
}
