using System.IO;
using Newtonsoft.Json;

namespace Retail_PointOfSales.Model;

public class OpeningFunds
{
    string filePath = Path.Combine(AppContext.BaseDirectory, @"..\..\..\JSON\openingFunds.json");
    public decimal Total { get; set; }
    public string Date { get; set; }
    
    public void SaveOpeningFunds(OpeningFunds fundData)
    { 
        using StreamWriter fileToWrite = new StreamWriter(filePath);
        JsonSerializer serializer = new JsonSerializer();
        // Save the JSON string to a file
        serializer.Serialize(fileToWrite, fundData);
    }
}