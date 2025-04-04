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
        SaleManager salesManager = new(); // instanciate a new class of SaleManager
        OpeningFunds openingFunds = new OpeningFunds(); // instanciate a new class of OpeningFunds
        EndOfDay newEndOfDay = new();


        public EndofDay()
        {
            InitializeComponent();
            OpenFundReturn(); //initialize this method when loading this window
            CalculateCashSales(); //initialize this method when loading this window
            CalculateCreditSales(); //initialize this method when loading this window
            TotalSales(); //initialize this method when loading this window
            CalculateExpectedCash(); //initialize this method when loading this window

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

                // Calculate total using integer values -- This variable was changed to a public property
                decimal total = (fiveCents * 0.05m) + (tenCents * 0.10m) + (twentyFiveCents * 0.25m) +
                                 oneDollar + (twoDollar * 2) +
                                 (fiveDollar * 5) + (tenDollar * 10) +
                                 (twentyDollar * 20) + (fiftyDollar * 50) +
                                 (hundredDollar * 100);

                // Display the total
                TotalTextBlock.Text = total.ToString("C");
                EndOfDayAuxiliaryTotalCash(); //updates the total cash value in this method every time total cash count text changes
                EndOfDayVariance(); //updates the value of variance every time totalcash count text changes
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

        //Method to calculate the cash sales for the day. data retrieving from sales.json file
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

            // Display the total cash sales
            TotalCashSales.Text = sumTotalValues.ToString("C");
        }

        // Method to calculate the variance between the cash sales and the cash counted (total cash count)
        private void EndOfDayVariance() 
        {
            var endOfDayTotalCash = EndOfDayAuxiliaryTotalCash();
            var endOfDayExpectedCash = EndOfDayAuxiliaryExpectedCash();
            var variance = endOfDayTotalCash - endOfDayExpectedCash;
            if(variance < 0)
            {
                Variance.Foreground = System.Windows.Media.Brushes.Red;
            }
            else
            {
                Variance.Foreground = System.Windows.Media.Brushes.LightGoldenrodYellow;
            }
            Variance.Text = variance.ToString("C");
        }

        private decimal EndOfDayAuxiliaryTotalCash()
        {
            var totalcash = TotalTextBlock.Text;
            return ToDecimal(totalcash);
        }

        private decimal EndOfDayAuxiliaryTotalCashSales()
        {
            var totalCashSales = TotalCashSales.Text;
            return ToDecimal(totalCashSales);
        }

        private decimal EndOfDayAuxiliaryTotalCreditSales()
        {
            var totalCreditSales = TotalCreditSales.Text;
            return ToDecimal(totalCreditSales);
        }

        private decimal EndOfDayAuxiliaryOpeningFund()
        {
            var openingFund = OpeningFund.Text;
            return ToDecimal(openingFund);
        }
        private decimal EndOfDayAuxiliaryExpectedCash()
        {
            var expectedCash = ExpectedCash.Text;
            return ToDecimal(expectedCash);
        }

        // Convert string to decimal
        private static decimal ToDecimal(string value)
        {
            //Console.WriteLine(decimal.Parse(value, System.Globalization.NumberStyles.Currency)); 
            return decimal.Parse(value, System.Globalization.NumberStyles.Currency);
            
        }

        //Method to calculate the credit sales for the day - data retrieving from sales.json file
        private decimal CalculateCreditSales()
        {
            // Get the sales data from the JSON file
            var sales = salesManager.LoadAllSales();

            //Filters the sales data based on sales payed with CreditCard and the sale date is today
            IEnumerable<Sale> creditSales = sales.Where(s => s.PaymentMethod == "CreditCard" && DateTime.TryParse(s.SaleDate, out DateTime saleDate) && saleDate.Date == DateTime.Now.Date);

            // Retrieve the Total data from the filtered creditSales
            var totalValues = creditSales.Select(s => s.Total).ToList();

            // Calculate the total CreditCard sales
            decimal sumTotalValues = (decimal)totalValues.Sum();
            //Console.WriteLine(sumTotalValues); //This line is to test the sumTotalValues on console
            // Display the total CreditCard sales
            TotalCreditSales.Text = sumTotalValues.ToString("C");

            return sumTotalValues;
        }

        // Method to calculate the total sales for the day - data retrieving from sales.json file
        private void TotalSales()
        {
            var sales = salesManager.LoadAllSales();
            IEnumerable<Sale> TotalSales = sales.Where(s => DateTime.TryParse(s.SaleDate, out DateTime saleDate) && saleDate.Date == DateTime.Now.Date);
            var totalValues = TotalSales.Select(s => s.Total).ToList();
            var cashSales = EndOfDayAuxiliaryTotalCashSales();
            var creditSales = EndOfDayAuxiliaryTotalCreditSales();
            //Checks if the sum of cash and credit sales field in this windows matches the total sales value in the saved history file
            if (cashSales + creditSales == totalValues.Sum())
            {
                TotalSalesValue.Text = "Total Sales: " + totalValues.Sum().ToString("C");
            }
            else
            {
                MessageBox.Show("The total sales value does not match the sum of cash and credit sales in the saved history. Please check database",
                                "Total Sales Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }

        private void CalculateExpectedCash()
        {
            var totalExpectedCash = EndOfDayAuxiliaryOpeningFund() + EndOfDayAuxiliaryTotalCashSales();
            ExpectedCash.Text = totalExpectedCash.ToString("C");
        }

        // Method to return the opening fund of the day, daved in the openingFunds.json file
        // into the endOfDay window
        private void OpenFundReturn()
        {
            // Get the open openFund data from the JSON file
            var openFund = openingFunds.LoadAllOpenFunds();

            //Filters the openFund data based on date is today
            IEnumerable<OpeningFunds> openFundDay = openFund.Where(s => DateTime.TryParse(s.Date, out DateTime date) && date.Date == DateTime.Now.Date);

            // Retrieve the Total data from the filtered openFundDay
            var totalFunds = openFundDay.Select(s => s.Total).ToList();

            // Calculate the total cash sales
            decimal sumTotalFunds = (decimal)totalFunds.Sum();

            // Display the total cash sales
            OpeningFund.Text = sumTotalFunds.ToString("C");
        }

        private void PostButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create an instance of endOfDayData
                EndOfDay endOfDayData = new EndOfDay
                {
                    Date = DateTime.Now.ToString("yyyy-MM-dd"),
                    OpeningFundEd = EndOfDayAuxiliaryOpeningFund(),
                    CashSalesEd = EndOfDayAuxiliaryTotalCashSales(),
                    ExpectedCashEd = EndOfDayAuxiliaryExpectedCash(),
                    TotalCashCountEd = EndOfDayAuxiliaryTotalCash(),
                    VarianceEd = EndOfDayAuxiliaryTotalCash() - EndOfDayAuxiliaryExpectedCash(),
                    CreditCardSalesEd = EndOfDayAuxiliaryTotalCreditSales(),
                    TotalSalesEd = EndOfDayAuxiliaryTotalCashSales() + EndOfDayAuxiliaryTotalCreditSales()
                };

                newEndOfDay.SaveEndOfDay(endOfDayData); // Save the endOfDay data to JSON file

                MessageBox.Show("End of day data has been saved successfully.", "Save Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // If something goes wrong during the saving operation show a message
                MessageBox.Show($"An error occurred while saving the end of day data.\n\n" +
                                $"Details: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
