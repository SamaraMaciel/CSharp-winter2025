namespace Retail_PointOfSales;

// User class
public class User(string userName, string password, string token)
{
    // Property to get or set the user's name
    public string UserName { get; set; } = userName;

    // Property to get or set the user's password
    public string Password { get; set; } = password;

    // Property to get or set the token associated with the user. 
    // Token is used to decode the user password.
    public string Token { get; set; } = token;
}
