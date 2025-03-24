namespace Retail_PointOfSales;

public class Sale
{
    public int SaleId { get; set; }
    public List<Product> Products { get; set; } = [];
    public decimal Total { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}

public enum PaymentMethod 
{
    Cash = 0,
    CreditCard = 1,
}
