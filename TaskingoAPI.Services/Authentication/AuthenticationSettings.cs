using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskingoAPI.Services.Authentication
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwtExpireHours { get; set; }
        public string JwtIssuer { get; set; }
    }
}
