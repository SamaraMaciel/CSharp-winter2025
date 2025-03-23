using System.IO;
using System.Text.Json;

namespace Retail_PointOfSales;

// User class
public class User(string userName, string password, string token)
{
    public string UserName { get; set; } = userName;
    public string Password { get; set; } = password;
    public string Token { get; set; } = token;// In this context, token is a prop the is used to decode the user password
}