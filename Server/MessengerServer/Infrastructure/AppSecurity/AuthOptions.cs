using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.AppSecurity
{
    public class AuthOptions
    {
        public const string ISSUER = "MessengerServer"; 
        public const string AUDIENCE = "MessengerClient";
        const string KEY = "mysupersecret_secretkey!123";
        public const int LIFETIME = 10;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
