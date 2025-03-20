using System.Security.Cryptography;

namespace Retail_PointOfSales;
//TODO: REMOVE THIS FILE BEFORE HANDING IN//
//TODO: REMOVE THIS FILE BEFORE HANDING IN//
//TODO: REMOVE THIS FILE BEFORE HANDING IN//
//TODO: REMOVE THIS FILE BEFORE HANDING IN//
public class PasswordHelper
{
    public static (string Hash, string Salt) HashPassword(string password)
    {
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
        {
            byte[] hash = pbkdf2.GetBytes(32);
            return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
        }
    }
}