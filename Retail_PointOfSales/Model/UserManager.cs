using System.IO;
using System.Security.Cryptography;

namespace Retail_PointOfSales;

// UserManager class
public class UserManager
{
    // Path to the JSON file that contains the user information
    private static readonly string path = Path.Combine(AppContext.BaseDirectory, @"..\..\..\JSON\user.json");
    private List<User> Users { get; set; } // List to store all the users from the JSON file

    // Class constructor
    public UserManager()
    {
        ReadUserFromFile();
    }
    
    /// <summary>
    /// Attempts to authenticate a user by verifying their username and password.
    /// </summary>
    /// <param name="username">The username entered by the user.</param>
    /// <param name="password">The password entered by the user.</param>
    /// <returns>The authenticated User object if login is successful; otherwise, null.</returns>
    public User LoginUser(string username, string password)
    {
        // Find the exact user, since we can't have multiple users with the same userName
        User user = Users.Find(u => u.UserName == username);
        
        // Check if a user was found and call the VerifyPassword to check if the password matches.
        // Three arguments are being passed, 1st the typed password, 2nd the storedPassword, 3rd the stored token.
        if (user != null && VerifyPassword(password, user.Password, user.Token))
        {
            return user; // If it's ok, then return the user and login
        }
        return null; // if not, return null and show a message.
    }

    /// <summary>
    /// Reads the JSON file containing user data and populates the Users list.
    /// If the file does not exist, initializes an empty list.
    /// </summary>
    private void ReadUserFromFile()
    {
        // Check if the file exists in the specified path.
        if (File.Exists(path))
        {
            // TextReader that reads characters from a byte stream in a particular encoding
            using StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd(); // read and store all the content in the json variable
            // Deserialize the JSON file to the User type object.
            Users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(json);
        }
        else
        {
            // If the file does not exist, return an empty list of users.
            Users = new List<User>();
        }
    }
    
    /// <summary>
    /// Verifies if the provided password matches the stored hashed password using PBKDF2.
    /// </summary>
    /// <param name="password">The password entered by the user.</param>
    /// <param name="storedPassword">The previously hashed password stored in the JSON file.</param>
    /// <param name="storedToken">The token that was used when hashing the stored password.</param>
    /// <returns>True if the password is correct; otherwise, false.</returns>
    private static bool VerifyPassword(string password, string storedPassword, string storedToken)
    {
        // Convert the stored base64-encoded token back into a byte array
        byte[] token = Convert.FromBase64String(storedToken);

        // Create a PBKDF2 object with the user-provided password, the stored token, 
        // 100,000 iterations, and SHA256 as the hash algorithm.
        using var pbkdf2 = new Rfc2898DeriveBytes(password, token, 100000, HashAlgorithmName.SHA256);

        // Generate a 32-byte hash from the provided password and stored token
        byte[] hashBytes = pbkdf2.GetBytes(32);

        // Convert the computed hash into a base64 string and compare it with the stored hashed password
        return Convert.ToBase64String(hashBytes) == storedPassword;
    }
}