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
        // The sale being closed, passed as an argument to the constructor
        Sale closingSale;

        /// <summary>
        /// Initializes a new instance of the CashPayment window.
        /// Sets the initial values for subtotal and total based on the passed sale.
        /// </summary>
        /// <param name="sale">The sale being processed.</param>
        public CashPayment(Sale sale)
        {
            closingSale = sale; // Set the passed sale to the closingSale variable
            InitializeComponent(); // Initialize the window's components
            SubtotalSalesTextBox.Text = closingSale.Subtotal.ToString(); // Set the subtotal text box
            TotalTextBox.Text = closingSale.Total.ToString(); // Set the total text box
        }

        /// <summary>
        /// Calculates the total after applying a discount entered by the user.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CalculateTotal(object sender, TextChangedEventArgs e)
        {
            // Initially set the values for Total and Cash Tendered
            TotalTextBox.Text = closingSale.Total.ToString();
            CashTenderedTextBox.Text = "0.00"; // Default value for cash tendered
            ChangeTextBox.Text = "0.00"; // Default value for change

            // Get the entered discount value
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
                MessageBox.Show("Total discount cannot be greater than subtotal.");
                DiscountTextBox.Text = ""; // Clear the discount input
                TotalTextBox.Text = closingSale.Total.ToString(); // Reset total
                return;
            }

            // Calculate the new total after applying the discount
            decimal newTotal = closingSale.Subtotal - convertedValue;
            TotalTextBox.Text = newTotal.ToString(); // Set the new total
        }

        /// <summary>
        /// Handles the click event of the Enter button to finalize the sale.
        /// Validates the inputs and saves the sale data.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the entered discount and cash tendered values
            string discount = DiscountTextBox.Text;
            string cashTendered = CashTenderedTextBox.Text;

            // Validate that both fields are filled
            if (string.IsNullOrWhiteSpace(discount) || string.IsNullOrWhiteSpace(cashTendered))
            {
                MessageBox.Show("Discount and Cash tendered are required fields.");
                return;
            }

            // Create a new Sale object with the current details
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
                SaleDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") // Set the current date and time
            };

            // Save the sale using the SaleManager class
            SaleManager saleManager = new SaleManager();
            saleManager.SaveSale(sale);
            DialogResult = true; // Indicate that the dialog is successful
            Close(); // Close the window
        }

        /// <summary>
        /// Handles the click event of the Cancel button to close the CashPayment window without saving.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Close the window without saving
        }

        /// <summary>
        /// Calculates the change to be given back based on the cash tendered.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CalculateChange(object sender, TextChangedEventArgs e)
        {
            decimal total = decimal.Parse(TotalTextBox.Text); // Get the total amount from the Total text box
            string cashTendered = CashTenderedTextBox.Text; // Get the entered cash tendered amount

            // Validate the entered cash tendered: Ensure it's a valid decimal or empty string
            if (string.IsNullOrWhiteSpace(cashTendered) || Regex.IsMatch(cashTendered, @"[^0-9.]"))
            {
                CashTenderedTextBox.Text = ""; // Clear the cash tendered input
                return; // Return early if invalid
            }

            // Attempt to convert the entered cash tendered value to decimal
            if (!decimal.TryParse(cashTendered, out var convertedValue))
            {
                // If conversion fails, show an error message
                MessageBox.Show("Invalid cash tendered value.");
                return;
            }

            // Calculate the change and display it in the Change text box
            ChangeTextBox.Text = (convertedValue - total).ToString();
        }
    }
}

