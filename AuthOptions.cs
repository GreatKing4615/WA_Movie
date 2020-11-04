using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace kinopoisk
{
    public class AuthOptions
    {
        public const string ISSUER = "localdb"; // издатель токена
        public const string AUDIENCE = "https://localhost:44320/"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
