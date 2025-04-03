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
        // The sale being closed, passed as an argument to the constructor
        Sale closingSale;

        /// <summary>
        /// Initializes a new instance of the CreditPayment window.
        /// Sets the initial values for subtotal and total based on the passed sale.
        /// </summary>
        /// <param name="sale">The sale being processed.</param>
        public CreditPayment(Sale sale)
        {
            closingSale = sale; // Set the passed sale to the closingSale variable
            InitializeComponent(); // Initialize the window's components
            SubtotalTextBox.Text = closingSale.Subtotal.ToString(); // Set the subtotal text box
            TotalTextBox.Text = closingSale.Total.ToString(); // Set the total text box
        }

        /// <summary>
        /// Handles the click event of the Cancel button to close the CreditPayment window without saving.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Close the window without saving
        }

        /// <summary>
        /// Calculates the total after applying a discount entered by the user.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CalculateTotal(object sender, TextChangedEventArgs e)
        {
            // Get the entered discount value from the DiscountTextBox
            string enteredDiscount = DiscountTextBox.Text;

            // Validate the entered discount: Ensure it's a valid decimal or empty string
            if (string.IsNullOrWhiteSpace(enteredDiscount) || Regex.IsMatch(enteredDiscount, @"[^0-9.]"))
            {
                // If the discount format is incorrect, clear the discount field
                DiscountTextBox.Text = "";
                return;
            }

            // Attempt to convert entered discount to decimal
            if (!decimal.TryParse(enteredDiscount, out var convertedValue))
            {
                // If conversion fails, show an error message
                MessageBox.Show("Invalid discount value.");
                return;
            }

            // Check if the entered discount is greater than the subtotal
            if (convertedValue > closingSale.Subtotal)
            {
                // If discount is greater than the subtotal, show a message and reset fields
                MessageBox.Show("Total discount cannot be greater than subtotal.");
                DiscountTextBox.Text = "";  // Clear the discount input
                TotalTextBox.Text = closingSale.Total.ToString();  // Reset total
                return;
            }

            // Calculate the new total after applying the discount
            decimal newTotal = closingSale.Subtotal - convertedValue;
            TotalTextBox.Text = newTotal.ToString();  // Set the new total value
        }

        /// <summary>
        /// Handles the click event of the Enter button to finalize the sale.
        /// Validates the inputs and saves the sale data.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the entered discount and credit card number values
            string discount = DiscountTextBox.Text;
            string creditCardText = CreditCardNoTextBox.Text;

            // Validate that both fields (discount and credit card number) are filled
            if (string.IsNullOrWhiteSpace(discount) || string.IsNullOrWhiteSpace(creditCardText))
            {
                MessageBox.Show("Discount and Credit card number are required fields.");
                return; // Return early if validation fails
            }

            // Mask the credit card number for security (only show the last 4 digits)
            string creditCardToSave = new string('x', 8) + creditCardText.Substring(8);

            // Create a new Sale object with the current details
            Sale sale = new Sale
            {
                SaleId = closingSale.SaleId,
                Products = closingSale.Products,
                PaymentMethod = closingSale.PaymentMethod,
                Subtotal = closingSale.Subtotal,
                Discount = decimal.Parse(DiscountTextBox.Text),
                Total = decimal.Parse(TotalTextBox.Text),
                CreditCardNumber = creditCardToSave, // Save the masked credit card number
                SaleDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") // Set the current date and time
            };

            // Save the sale using the SaleManager class
            SaleManager saleManager = new SaleManager();
            saleManager.SaveSale(sale);
            DialogResult = true; // Indicate that the dialog was successful
            Close(); // Close the window
        }
    }
}

