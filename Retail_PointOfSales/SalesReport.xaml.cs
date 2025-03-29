using System.Windows;
using Retail_PointOfSales.Model;

namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for SalesReport.xaml
    /// </summary>
    public partial class SalesReport : Window
    {
        public SalesReport()
        {
            InitializeComponent();
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            string startDate = StartDatePicker.Text;
            string endDate = EndDatePicker.Text;
            
            if (string.IsNullOrWhiteSpace(EndDatePicker.Text))
            {
                endDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            
            SaleManager saleManager = new SaleManager();
            saleManager.FilterSales(startDate, endDate);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // For configuration
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
