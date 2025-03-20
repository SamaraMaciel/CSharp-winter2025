using System.IO;
using System.Text.Json;

namespace Retail_PointOfSales;

// User class
public class User(string userName, string password, string salt)
{
    public string UserName { get; set; } = userName;
    public string Password { get; set; } = password;
    public string Salt { get; set; } = salt;// In this context, salt is a prop the is used to decode the user password
}