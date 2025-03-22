using System.IO;

namespace Retail_PointOfSales;

public class ProductManager
{
    private static readonly string path = Path.Combine(AppContext.BaseDirectory, @"..\..\..\JSON\products.json");
    
    public List<Product> LoadAllProducts()
    {
        if (File.Exists(path))
        {
            using StreamReader sr = new StreamReader(path);
            string json = sr.ReadToEnd();
            return  Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(json);
        }
        
        return new List<Product>();
    }

    public object Search(string searchText)
    {
        throw new NotImplementedException();
    }
}