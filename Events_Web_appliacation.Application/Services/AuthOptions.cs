using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Events_Web_application_DataBase.Services
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthVadim"; // издатель токена
        public const string AUDIENCE = "MyAuthUser"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123sddfgsdgfsdfgdf";   // ключ для шифрации
        public const int LIFETIME = 10; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
