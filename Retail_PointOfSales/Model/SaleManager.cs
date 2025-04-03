using System.IO;
using Newtonsoft.Json;

namespace Retail_PointOfSales.Model;

//SaleManager class, a helper class to handle serialization and deserialization of JSON files
public class SaleManager
{
    // The file path for storing sales data in JSON format
    string filePath = Path.Combine(AppContext.BaseDirectory, @"..\..\..\JSON\sales.json");

    // List to hold the sales data
    List<Sale> sales = new List<Sale>();

    /// <summary>
    /// Initializes a new instance of the SaleManager class. 
    /// It loads all sales when a new instance is created.
    /// </summary>
    public SaleManager()
    {
        LoadSales(); // Loads sales from the file
        LoadAllSales(); // Loads the LoadAllSales() method when a new instance of the SaleManager class is created
    }

    /// <summary>
    /// Loads sales data from a JSON file into the sales list.
    /// </summary>
    public void LoadSales()
    {
        // Check if the sales file exists
        if (File.Exists(filePath))
        {
            // Open the file to read its contents
            using StreamReader fileToRead = new StreamReader(filePath);
            string json = fileToRead.ReadToEnd();
            
            // If the file is not empty, deserialize the JSON data into the sales list
            if (!string.IsNullOrEmpty(json))
            {
                sales = JsonConvert.DeserializeObject<List<Sale>>(json) ?? new List<Sale>();
            }
        }
    }

    /// <summary>
    /// Adds a new sale to the sales list and saves the updated list to the JSON file.
    /// </summary>
    public void SaveSale(Sale sale)
    {
        // Add the new sale to the sales list
        sales.Add(sale);

        // Open the file to write the updated sales list
        using StreamWriter fileToWrite = new StreamWriter(filePath);
        JsonSerializer serializer = new JsonSerializer();
        
        // Serialize and save the updated sales list to the file
        serializer.Serialize(fileToWrite, sales);
    }

    /// <summary>
    /// Filters the sales list based on the specified start and end dates.
    /// </summary>
    /// <param name="startDate">The start date for filtering sales.</param>
    /// <param name="endDate">The end date for filtering sales.</param>
    /// <returns>A list of sales that fall within the specified date range.</returns>
    public List<Sale> FilterSales(string startDate, string endDate)
    {
        // Find sales within the date range using the StartsWith method
        List<Sale> filteredSales = sales.FindAll(sale => sale.SaleDate.StartsWith(startDate) || sale.SaleDate.StartsWith(endDate));
        
        // Output the number of filtered sales to the console
        Console.WriteLine(filteredSales.Count);

        // Return the list of filtered sales
        return filteredSales;
    }

    /// <summary>
    /// Loads all sales from the JSON file and returns them as a list of sales.
    /// </summary>
    /// <returns>A list of all sales loaded from the file.</returns>
    public List<Sale> LoadAllSales()
    {
        // Initialize an empty list to hold the sales data
        List<Sale> salesList = new List<Sale>();

        // Check if the file exists at the specified file path
        if (File.Exists(filePath))
        {
            // Open the file to read its contents
            using StreamReader fileToRead = new StreamReader(filePath);
            string json = fileToRead.ReadToEnd();

            // If the file is not empty, deserialize the JSON content into a list of Sale objects
            if (!string.IsNullOrEmpty(json))
            {
                salesList = JsonConvert.DeserializeObject<List<Sale>>(json) ?? new List<Sale>();
            }
        }

        // Return the list of sales
        return salesList;
    }
}
