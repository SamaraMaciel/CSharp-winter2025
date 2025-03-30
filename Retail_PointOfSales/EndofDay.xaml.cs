using Retail_PointOfSales.Model;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for EndofDay.xaml
    /// </summary>
    public partial class EndofDay : Window
    {
        private List<Sale> sales; // work in progress - SM
        SaleManager salesManager = new(); // work in progress - SM
        

        public EndofDay()
        {
            InitializeComponent();
            //initialize the CalculateCashSales method when loading this window
            CalculateCashSales();
            EndOfDayVariance(); //initialize the EndOfDayVariance method when loading the window
            //TextBox_TextChanged();
        }

        // Automatically calculate and display the sum value in the Total_TextBox
        private void CalculateTotal(object sender, TextChangedEventArgs e)
        {
            try
            {
                // Validation: Whole number input only 
                if (!WholeNumber(FiveCentsTextBox.Text) ||
                    !WholeNumber(TenCentsTextBox.Text) ||
                    !WholeNumber(TwentyFiveCentsTextBox.Text) ||
                    !WholeNumber(OneDollarTextBox.Text) ||
                    !WholeNumber(TwoDollarTextBox.Text) ||
                    !WholeNumber(FiveDollarTextBox.Text) ||
                    !WholeNumber(TenDollarTextBox.Text) ||
                    !WholeNumber(TwentyDollarTextBox.Text) ||
                    !WholeNumber(FiftyDollarTextBox.Text) ||
                    !WholeNumber(HundredDollarTextBox.Text))
                {
                    MessageBox.Show("Invalid input. Please enter whole numbers only (no decimals or letters).",
                                    "Input Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    return;
                }
                // Coin values
                int fiveCents = string.IsNullOrWhiteSpace(FiveCentsTextBox.Text) ? 0 : int.Parse(FiveCentsTextBox.Text);
                int tenCents = string.IsNullOrWhiteSpace(TenCentsTextBox.Text) ? 0 : int.Parse(TenCentsTextBox.Text);
                int twentyFiveCents = string.IsNullOrWhiteSpace(TwentyFiveCentsTextBox.Text) ? 0 : int.Parse(TwentyFiveCentsTextBox.Text);
                int oneDollar = string.IsNullOrWhiteSpace(OneDollarTextBox.Text) ? 0 : int.Parse(OneDollarTextBox.Text);
                int twoDollar = string.IsNullOrWhiteSpace(TwoDollarTextBox.Text) ? 0 : int.Parse(TwoDollarTextBox.Text);

                // Notes values
                int fiveDollar = string.IsNullOrWhiteSpace(FiveDollarTextBox.Text) ? 0 : int.Parse(FiveDollarTextBox.Text);
                int tenDollar = string.IsNullOrWhiteSpace(TenDollarTextBox.Text) ? 0 : int.Parse(TenDollarTextBox.Text);
                int twentyDollar = string.IsNullOrWhiteSpace(TwentyDollarTextBox.Text) ? 0 : int.Parse(TwentyDollarTextBox.Text);
                int fiftyDollar = string.IsNullOrWhiteSpace(FiftyDollarTextBox.Text) ? 0 : int.Parse(FiftyDollarTextBox.Text);
                int hundredDollar = string.IsNullOrWhiteSpace(HundredDollarTextBox.Text) ? 0 : int.Parse(HundredDollarTextBox.Text);

                // Calculate total using integer values
                decimal total = (fiveCents * 0.05m) + (tenCents * 0.10m) + (twentyFiveCents * 0.25m) +
                                 oneDollar + (twoDollar * 2) +
                                 (fiveDollar * 5) + (tenDollar * 10) +
                                 (twentyDollar * 20) + (fiftyDollar * 50) +
                                 (hundredDollar * 100);

                // Display the total
                TotalTextBlock.Text = total.ToString("C");
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input. Please enter whole numbers only (no decimals or letters).",
                "Input Error",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            }
        }

        // Validate empty input or valid whole number
        private bool WholeNumber(string input)
        {
            return string.IsNullOrWhiteSpace(input) || int.TryParse(input, out _);
        }

        // Close EndofDay Window
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Method to calculate the cash sales for the day - data retrieving from sales.json file
        private void CalculateCashSales()
        {
            // Get the sales data from the JSON file
            var sales = salesManager.LoadAllSales();

            //Filters the sales data based on sales payed with cash and the sale date is today
            IEnumerable<Sale> cashSales = sales.Where(s => s.PaymentMethod == "Cash" && DateTime.TryParse(s.SaleDate, out DateTime saleDate) && saleDate.Date == DateTime.Now.Date);

            // Retrieve the Total data from the filtered cashSales
            var totalValues = cashSales.Select(s => s.Total).ToList();

            // Calculate the total cash sales
            decimal sumTotalValues = (decimal)totalValues.Sum();
            //Console.WriteLine(sumTotalValues); //This line is to test the sumTotalValues on console
            // Display the total cash sales
            TotalCashSales.Text = sumTotalValues.ToString("C");
        }

        //TotalTextBlock.TextChanged += EndOfDayVariance(); // Event handler for the EndOfDayVariance method
        // Mtethod to calculate the variance between the cash sales and the cash counted (total cash count)
        private void EndOfDayVariance() //work in prgress - SM needs to catch the value of TotalTextBlock every time it changes
        {
            var endOfDayTotalCash = TotalTextBlock.Text;
            var edOfDayCashSales = TotalCashSales.Text;
            var variance = ToDecimal(edOfDayCashSales) - ToDecimal(endOfDayTotalCash);
            //var variance = decimal.ParseString(edOfDayCashSales) - decimal.ParseString(endOfDayTotalCash);
            Variance.Text = variance.ToString("C");
        }

        // Convert string to decimal
        private static decimal ToDecimal(string value)
        {
            Console.WriteLine(decimal.Parse(value, System.Globalization.NumberStyles.Currency)); 
            return decimal.Parse(value, System.Globalization.NumberStyles.Currency);
            
        }

        //Fix it later - colect the data from the TotalTextBlock every time it changes
        // is possibility to add TextChanged="EndOfDayVariance" to the TotalTextBlock in the xmal file
        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    EndOfDayVariance(sender, e);
        //}

    }
}
