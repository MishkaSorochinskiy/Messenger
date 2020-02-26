using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.AppSecurity
{
    public class AuthOptions
    {
        public const string ISSUER = "MessengerServer"; 
        public const string AUDIENCE = "AuthClient";
        const string KEY = "mysuperpupersecretkey_12345";
        public const int LIFETIME = 10;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
