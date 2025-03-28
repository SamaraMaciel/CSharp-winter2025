using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Retail_PointOfSales.Model;

namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for CashPayment.xaml
    /// </summary>
    public partial class CashPayment : Window
    {
        Sale closingSale;
        public CashPayment(Sale sale)
        {
            closingSale = sale;
            InitializeComponent();
            SubtotalSalesTextBox.Text = closingSale.Subtotal.ToString();
            TotalTextBox.Text = closingSale.Total.ToString();
        }
        
        private void CalculateTotal(object sender, TextChangedEventArgs e)
        {
            // Initially set the values for Total and Cash Tendered
            TotalTextBox.Text = closingSale.Total.ToString();
            CashTenderedTextBox.Text = "0.00";
            ChangeTextBox.Text = "0.00";

            // Get the entered discount
            string enteredDiscount = DiscountTextBox.Text;

            // Validate the entered discount: Ensure it's a valid decimal or empty string
            if (string.IsNullOrWhiteSpace(enteredDiscount) || Regex.IsMatch(enteredDiscount, @"[^0-9.]"))
            {
                // If check fails, show a message
                DiscountTextBox.Text = "";
                return;
            }

            // Attempt to convert entered discount to decimal
            if (!decimal.TryParse(enteredDiscount, out var convertedValue))
            {
                // If conversion fails, show a message
                MessageBox.Show("Invalid discount value.");
                return;
            }

            // Check if the entered discount is greater than the subtotal
            if (convertedValue > closingSale.Subtotal)
            {
                MessageBox.Show("Total discount cannot be greater than subtotal.");
                DiscountTextBox.Text = "";  // Clear the discount input
                TotalTextBox.Text = closingSale.Total.ToString();  // Reset total
                return;
            }

            // Calculate the new total after applying the discount
            decimal newTotal = closingSale.Subtotal - convertedValue;
            TotalTextBox.Text = newTotal.ToString();  // Set the new total
        }

        // Event handler for Enter button click, 
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            Sale sale = new Sale
            {
                SaleId = closingSale.SaleId,
                Products = closingSale.Products,
                PaymentMethod = closingSale.PaymentMethod,
                Subtotal = closingSale.Subtotal,
                Discount = decimal.Parse(DiscountTextBox.Text),
                Total = decimal.Parse(TotalTextBox.Text),
                CashTendered = decimal.Parse(CashTenderedTextBox.Text),
                Change = decimal.Parse(ChangeTextBox.Text),
                SaleDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")
            };
            
            SaleManager saleManager = new SaleManager();
            saleManager.SaveSale(sale);
            Close();
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void CalculateChange(object sender, TextChangedEventArgs e)
        {
            decimal total = decimal.Parse(TotalTextBox.Text);
            string cashTendered = CashTenderedTextBox.Text;
            // Validate the entered discount: Ensure it's a valid decimal or empty string
            if (string.IsNullOrWhiteSpace(cashTendered) || Regex.IsMatch(cashTendered, @"[^0-9.]"))
            {
                CashTenderedTextBox.Text = "";
                return;  // Return early if invalid
            }
            // Attempt to convert entered discount to decimal
            if (!decimal.TryParse(cashTendered, out var convertedValue))
            {
                // If conversion fails, show a message
                MessageBox.Show("Invalid discount value.");
                return;
            }
            ChangeTextBox.Text = (convertedValue - total).ToString();
        }
    }
}
