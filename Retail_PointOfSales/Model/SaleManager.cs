using System.IO;
using Newtonsoft.Json;

namespace Retail_PointOfSales.Model;

public class SaleManager
{
    public void SaveSale(Sale sale)
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, @"..\..\..\JSON\sales.json");
        
        List<Sale> sales = new List<Sale>();
        if (File.Exists(filePath))
        {
            using StreamReader fileToRead = new StreamReader(filePath);
            string json = fileToRead.ReadToEnd();
            if (!string.IsNullOrEmpty(json))
            {
                sales = JsonConvert.DeserializeObject<List<Sale>>(json) ?? [];
            }
        }
        
        sales.Add(sale);

        using StreamWriter fileToWrite = new StreamWriter(filePath);
        JsonSerializer serializer = new JsonSerializer();
        serializer.Serialize(fileToWrite, sales);
    }
}