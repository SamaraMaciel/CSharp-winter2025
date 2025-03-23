using System.Windows;
using System.Windows.Controls;

namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for OpeningFund.xaml

    public partial class OpeningFund : Window
    {
        public OpeningFund()
        {
            InitializeComponent();
        }

        // Automatically calculate and display the sum value in the Total_TextBox
        // Feel free to update the code
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

        private bool WholeNumber(string input)
        {
            // Validate empty input or valid whole number
            return string.IsNullOrWhiteSpace(input) || int.TryParse(input, out _);
        }

        // Save the opening fund to JSON file.  This is just a placeholder for Save buttonclick. 
        // Feel free to change it
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                MessageBox.Show($"Error: JSON FILE ");  
            }

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //// Create an instance of the Main Window
            //MainWindow mainWindow = new MainWindow();

            //// Show the Main Window
            //mainWindow.Show();

            // Close the current Opening Fund window
            this.Close();
        }
    }
}
