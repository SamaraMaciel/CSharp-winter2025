using System.IO;

namespace Retail_PointOfSales;

public class ProductManager
{
    private static readonly string path = Path.Combine(AppContext.BaseDirectory, @"..\..\..\JSON\products.json");
    private List<Product> Products { get; set; }

    public ProductManager()
    {
        LoadAllProducts();
    }
    
    public List<Product> LoadAllProducts()
    {
        if (File.Exists(path))
        {
            using StreamReader sr = new StreamReader(path);
            string json = sr.ReadToEnd();
            Products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(json);
            return Products;
        }
        
        return new List<Product>();
    }

    public object Search(string searchText)
    {
        var filteredProducts = Products
            .Where(p => p.ProductName.ToLower().Contains(searchText.ToLower())).ToList();
        return filteredProducts;
        
    }
}