namespace Retail_PointOfSales;

// Sale class
public class Sale
{
    // The unique identifier for the sale
    public string SaleId { get; set; }

    // List of products involved in the sale
    public List<Product> Products { get; set; } = new ();

    // The payment method used for the sale (Cash or Credit Card)
    public string PaymentMethod { get; set; }

    // The credit card number used for the payment (optional)
    public string? CreditCardNumber { get; set; }

    // The subtotal of the sale (total before any discounts)
    public decimal Subtotal { get; set; } // total right now

    // The discount applied to the sale (optional)
    public decimal? Discount { get; set; }

    // The total amount after applying discounts (grand total)
    public decimal Total { get; set; } // grand total

    // The cash tendered by the customer (optional)
    public decimal? CashTendered { get; set; }

    // The change to be given back to the customer (optional)
    public decimal? Change { get; set; }

    // The date when the sale took place
    public string SaleDate { get; set; }
}

// PaymentMethod enum, to use when doing the payment
public enum PaymentMethod
{
    // Payment made by cash
    Cash = 0,

    // Payment made by credit card
    CreditCard = 1,
}

// SaleReportItem class used to generate the report
public class SaleReportItem
{
    // The unique identifier for the sale report item
    public string SaleId { get; set; }

    // The date the sale occurred
    public string SaleDate { get; set; }

    // The name of the product involved in the sale
    public string Product { get; set; }

    // The total amount for this product in the sale
    public decimal Total { get; set; }
}
