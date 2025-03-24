using System.IO;

namespace Retail_PointOfSales;

/// <summary>
/// Manages product operations such as loading and searching products from a JSON file.
/// </summary>
public class ProductManager
{
    /// The file path to the JSON file containing product data.
    private static readonly string path = Path.Combine(AppContext.BaseDirectory, @"..\..\..\JSON\products.json");
    /// A list of all loaded products.
    private List<Product> Products { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductManager"/> class and loads all products.
    /// </summary>
    public ProductManager()
    {
        LoadAllProducts();
    }
    
    /// <summary>
    /// Loads all products from the JSON file.
    /// </summary>
    /// <returns>A list of <see cref="Product"/> objects if the file exists; otherwise, an empty list.</returns>
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

    /// <summary>
    /// Searches for products that match the given search text.
    /// </summary>
    /// <param name="searchText">The text to search for in product names.</param>
    /// <returns>A list of matching products or null if no matches are found.</returns>
    public List<Product> Search(string searchText)
    {
        var filteredProducts = Products
            .Where(p => p.ProductName.ToLower().Contains(searchText.ToLower())).ToList();
        return filteredProducts.Count == 0 ? null : filteredProducts;
    }
}