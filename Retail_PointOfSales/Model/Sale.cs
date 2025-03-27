namespace Retail_PointOfSales;

public class Sale
{
    public string SaleId { get; set; }
    public List<Product> Products { get; set; } = [];
    public PaymentMethod PaymentMethod { get; set; }
    public string? CreditCardNumber { get; set; }
    public decimal Subtotal { get; set; } // total rn
    public decimal? Discount { get; set; }
    public decimal Total { get; set; } // grand total
    public decimal? CashTendered { get; set; }
    public decimal? Change { get; set; }
}

public enum PaymentMethod 
{
    Cash = 0,
    CreditCard = 1,
}
