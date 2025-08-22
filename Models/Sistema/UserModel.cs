using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

namespace LeonXIIICore.Models.Sistema
{
    public class UserModel
    {
        public string Message { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public List<string> MemberOf { get; set; }

        public UserModel()
        {
            MemberOf = new List<string>();
        }
    }

    public class LoginRequest
    {
        public string username { get; set; }
        public string? password { get; set; }  // Optional, since anonymous login doesn't require password
    }

}
