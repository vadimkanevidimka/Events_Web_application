using System.Security.Cryptography;

namespace Events_Web_application_DataBase.Services
{
    public static class PasswordHash
    {
        public static string CalculateHash(this string somestring)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = System.Text.Encoding.Default.GetBytes(somestring);
                return String.Concat(sha256.ComputeHash(inputBytes).Select(item => item.ToString("x2")));
            }
        }
    }
}
