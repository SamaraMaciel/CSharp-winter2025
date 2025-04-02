using System.Globalization;
using System.Windows;
using Retail_PointOfSales.Model;

namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for SalesReport.xaml
    /// </summary>
    public partial class SalesReport
    {
        public SalesReport()
        {
            InitializeComponent();
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            string startDate = StartDatePicker.Text;
            if (string.IsNullOrWhiteSpace(startDate))
            {
                MessageBox.Show($"Start date cannot be empty.", "Start date empty", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            string endDate = EndDatePicker.Text;
    
            if (string.IsNullOrWhiteSpace(EndDatePicker.Text))
            {
                endDate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            SaleManager saleManager = new SaleManager();
            List<Sale> filteredSales = saleManager.FilterSales(startDate, endDate);

            // Flatten the data into SaleReportItem list
            List<SaleReportItem> reportItems = new List<SaleReportItem>();

            foreach (Sale sale in filteredSales)
            {
                foreach (Product product in sale.Products)
                {
                    string formattedSaleDate = DateTime.Parse(sale.SaleDate).ToString("yyyy-MM-dd HH:mm:ss");
                    reportItems.Add(new SaleReportItem
                    {
                        SaleId = sale.SaleId,
                        SaleDate = formattedSaleDate,
                        Product = product.ProductName,
                        Total = sale.Total
                    });
                }
            }

            // Bind the flattened data to the DataGrid
            SalesDataGrid.ItemsSource = reportItems;

            // Calculate the total sales and display it
            decimal totalSales = filteredSales.Sum(sale => sale.Total);
            TotalSalesText.Text = totalSales.ToString("C");  // Display as currency
        }
        
        private void SaveReportButton_Click(object sender, RoutedEventArgs e)
        {
            // Prompt the user for the file path using SaveFileDialog
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                FileName = "SalesReport.csv"  // Default file name
            };

            // Show the save file dialog and check if the user selected a file
            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                string filePath = saveFileDialog.FileName;

                // Use CsvHelper to write the report to CSV
                using (var writer = new System.IO.StreamWriter(filePath))
                using (var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    // Write the header
                    csv.WriteHeader<SaleReportItem>();
                    csv.NextRecord();

                    // Write the records
                    foreach (SaleReportItem item in SalesDataGrid.ItemsSource)
                    {
                        csv.WriteRecord(item);
                        csv.NextRecord();
                    }
                }

                //Inform the user that the file was saved successfully
                MessageBox.Show("Report saved successfully!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
