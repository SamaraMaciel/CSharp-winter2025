using System.IO;
using Newtonsoft.Json;

namespace Retail_PointOfSales.Model;

// OpeningFunds Class
public class OpeningFunds
{
    // Define the file path where the JSON data will be saved. The file is located relative to the application's base directory.
    // The Path.Combine method ensures correct path formation for different environments.
    string filePath = Path.Combine(AppContext.BaseDirectory, @"..\..\..\JSON\openingFunds.json");

    // Property to store the total opening fund value, calculated from the individual fund values.
    public decimal Total { get; set; }

    // Property to store the date when the opening funds are recorded.
    public string Date { get; set; }

    /// <summary>
    /// Saves the opening funds data to a JSON file at the specified file path.
    /// This method serializes the provided OpeningFunds object into a JSON formatted string
    /// and writes it to a file so it can be stored and used later.
    /// </summary>
    /// <param name="fundData">The OpeningFunds object that contains the data to be saved.</param>
    public void SaveOpeningFunds(OpeningFunds fundData)
    { 
        // Create a StreamWriter to write to the file specified by filePath.
        // Using 'using' ensures that the file will be properly closed after writing.
        using StreamWriter fileToWrite = new StreamWriter(filePath);

        // Create a JsonSerializer instance to handle the serialization of the object to JSON.
        JsonSerializer serializer = new JsonSerializer();

        // Serialize the OpeningFunds object (fundData) and write it to the file.
        // This converts the OpeningFunds object into a JSON formatted string and saves it in the file.
        serializer.Serialize(fileToWrite, fundData);
    }

    public List<OpeningFunds> LoadAllOpenFunds()
    {
        // Initialize an empty list to hold the open fund data
        List<OpeningFunds> openingFunds = new List<OpeningFunds>();

        // OpeningFunds openingFund = JsonConvert.DeserializeObject<OpeningFunds>(filePath);


        // Check if the file exists at the specified file path
        if (File.Exists(filePath))
        {
            // Open the file to read its contents
            using StreamReader fileToRead = new StreamReader(filePath);
            string json = fileToRead.ReadToEnd();

            // If the file is not empty, deserialize the JSON content into a list of Sale objects
            if (!string.IsNullOrEmpty(json))
            {
                //this line is returning an error, because json file is save in the format: {} as object and not [{}] as list
                //needs fixig - its throughing an error
                openingFunds = JsonConvert.DeserializeObject<List<OpeningFunds>>(json) ?? new List<OpeningFunds>();
            }
        }

        // Return the list of sales
        return openingFunds;

    }
}