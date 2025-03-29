using System.IO;
using Newtonsoft.Json;

namespace Retail_PointOfSales.Model;

public class SaleManager
{
    string filePath = Path.Combine(AppContext.BaseDirectory, @"..\..\..\JSON\sales.json");
    List<Sale> sales = [];

    private List<Sale> sales2 { get; set; } //List of Sale type that is being used in the LoadAllSales() method

    public SaleManager()
    {
        LoadSales();
        LoadAllSales(); // loads the LoadAllSales() method when a new instance of the SaleManager class is created
    }
    
    public void LoadSales()
    {
        if (File.Exists(filePath))
        {
            using StreamReader fileToRead = new StreamReader(filePath);
            string json = fileToRead.ReadToEnd();
            if (!string.IsNullOrEmpty(json))
            {
                sales = JsonConvert.DeserializeObject<List<Sale>>(json) ?? [];
            }
        }
    }
    
    public void SaveSale(Sale sale)
    {
        sales.Add(sale);

        using StreamWriter fileToWrite = new StreamWriter(filePath);
        JsonSerializer serializer = new JsonSerializer();
        serializer.Serialize(fileToWrite, sales);
    }

    public List<Sale> FilterSales(string startDate, string endDate)
    {
        List<Sale> filteredSales = sales.FindAll(sale => sale.SaleDate.StartsWith(startDate) || sale.SaleDate.StartsWith(endDate));
        Console.WriteLine(filteredSales.Count);
        return filteredSales;
    }

    // Method that reads all sales from the sales.json file
    public List<Sale> LoadAllSales()
    {
        //string filePath = Path.Combine(AppContext.BaseDirectory, @"..\..\..\JSON\sales.json");
        List<Sale> sales2 = new List<Sale>();
        if (File.Exists(filePath))
        {
            using StreamReader fileToRead = new StreamReader(filePath);
            string json = fileToRead.ReadToEnd();
            if (!string.IsNullOrEmpty(json))
            {
                sales2 = JsonConvert.DeserializeObject<List<Sale>>(json) ?? [];
            }
        }
        return sales2;
    }

}