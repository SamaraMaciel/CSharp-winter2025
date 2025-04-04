namespace Retail_PointOfSales
{
    // Product Class
    public class Product
    {
        // The unique identifier for the product
        public int ProductId { get; set; }

        // The name of the product
        public string ProductName { get; set; }

        // The price of the product
        public decimal ProductPrice { get; set; }

        // The quantity of the product available, defaults to 0
        public int Quantity { get; set; } = 0; // Default to 0

        // The total amount for the product, calculated as Quantity * ProductPrice
        public decimal Amount => Quantity * ProductPrice; // Auto-calculated
    }

}
