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
    /// Interaction logic for OpeningFund.xaml
    /// </summary>
    public partial class OpeningFund : Window
    {
        public OpeningFund()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Parse each coin textbox value
            decimal fiveCents = string.IsNullOrWhiteSpace(FiveCentsTextBox.Text) ? 0 : decimal.Parse(FiveCentsTextBox.Text) * 0.05m;
            decimal tenCents = string.IsNullOrWhiteSpace(TenCentsTextBox.Text) ? 0 : decimal.Parse(TenCentsTextBox.Text) * 0.10m;
            decimal twentyFiveCents = string.IsNullOrWhiteSpace(TwentyFiveCentsTextBox.Text) ? 0 : decimal.Parse(TwentyFiveCentsTextBox.Text) * 0.25m;
            decimal oneDollar = string.IsNullOrWhiteSpace(OneDollarTextBox.Text) ? 0 : decimal.Parse(OneDollarTextBox.Text);
            decimal twoDollars = string.IsNullOrWhiteSpace(TwoDollarTextBox.Text) ? 0 : decimal.Parse(TwoDollarTextBox.Text) * 2;

            // Calculate the total
            decimal total = fiveCents + tenCents + twentyFiveCents + oneDollar + twoDollars;

            // Update the TOTAL text block
            TotalTextBlock.Text = total.ToString("C");

            //Notes to be added
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the Main Window
            MainWindow mainWindow = new MainWindow();

            // Show the Main Window
            mainWindow.Show();

            // Close the current Opening Fund window
            this.Close();
        }
    }
}
