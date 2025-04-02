using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Retail_PointOfSales.Model;

namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for CreditPayment.xaml
    /// </summary>
    public partial class CreditPayment : Window
    {
        Sale closingSale;
        public CreditPayment(Sale sale)
        {
            closingSale = sale;
            InitializeComponent();
            SubtotalTextBox.Text = closingSale.Subtotal.ToString();
            TotalTextBox.Text = closingSale.Total.ToString();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CalculateTotal(object sender, TextChangedEventArgs e)
        {
            // TotalTextBox.Text = closingSale.Total.ToString();
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
        
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string discount = DiscountTextBox.Text;
            string creditCardText = CreditCardNoTextBox.Text;
            if (string.IsNullOrWhiteSpace(discount) || string.IsNullOrWhiteSpace(creditCardText))
            {
                MessageBox.Show("Discount and Credit card number are required fields.");
                return;   
            }
            
            string creditCardToSave = new string('x', 8) + creditCardText.Substring(8);
            Sale sale = new Sale
            {
                SaleId = closingSale.SaleId,
                Products = closingSale.Products,
                PaymentMethod = closingSale.PaymentMethod,
                Subtotal = closingSale.Subtotal,
                Discount = decimal.Parse(DiscountTextBox.Text),
                Total = decimal.Parse(TotalTextBox.Text),
                CreditCardNumber = creditCardToSave,
                SaleDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")
            };
            
            SaleManager saleManager = new SaleManager();
            saleManager.SaveSale(sale);
            DialogResult = true;
            Close();
        }
    }
}
