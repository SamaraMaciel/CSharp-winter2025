using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for CashPayment.xaml
    /// </summary>
    public partial class CashPayment : Window
    {
        public CashPayment()
        {
            InitializeComponent();
        }

        private void CalculateGrandTotalAndChange()
        {
            try
            {
                // Parse the values from the textboxes
                decimal totalSales = decimal.Parse(TotalSalesTextBox.Text);
                decimal discount = decimal.Parse(DiscountTextBox.Text);
                decimal cashTendered = decimal.Parse(CashTenderedTextBox.Text);

                // Calculate the Grand Total
                decimal grandTotal = totalSales - discount;
                GrandTotalTextBox.Text = grandTotal.ToString("0.00");

                // Calculate the Change
                decimal change = cashTendered - grandTotal;
                ChangeTextBox.Text = change.ToString("0.00");
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input. Please ensure all fields contain numeric values.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}"); 
            }
        }

        // Event handler for Enter button click, 
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {

            // Code to handle submission to JSON file
            // Fetch the GrandTotal Textbox to record the cash transactions
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
