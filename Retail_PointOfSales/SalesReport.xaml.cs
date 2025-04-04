using System.Globalization;
using System.Windows;
using Retail_PointOfSales.Model;

namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for SalesReport.xaml
    /// This class handles the interaction for generating and saving a sales report.
    /// </summary>
    public partial class SalesReport
    {
        /// <summary>
        /// Initializes the SalesReport window.
        /// </summary>
        public SalesReport()
        {
            InitializeComponent();  // Initialize the components of the window
        }

        /// <summary>
        /// Handles the click event for generating a report based on the selected date range.
        /// Filters sales based on the start and end date, flattens the data, and displays it in a DataGrid.
        /// </summary>
        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            string startDate = StartDatePicker.Text; // Get the start date from the StartDatePicker
            if (string.IsNullOrWhiteSpace(startDate))
            {
                // Show an error message if the start date is empty
                MessageBox.Show($"Start date cannot be empty.", "Start date empty", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;  // Exit the method if the start date is empty
            }
            
            string endDate = EndDatePicker.Text;  // Get the end date from the EndDatePicker
    
            if (string.IsNullOrWhiteSpace(EndDatePicker.Text))
            {
                // If the end date is empty, set it to the current date
                endDate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            // Create an instance of SaleManager to handle sale data
            SaleManager saleManager = new SaleManager();

            // Filter sales based on the provided start and end dates
            List<Sale> filteredSales = saleManager.FilterSales(startDate, endDate);

            if (filteredSales.Count == 0)
            {
                MessageBox.Show(
                    "No sales found for the informed period.", 
                    "No Sales Found", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Create a list to store flattened sale data in the form of SaleReportItem objects
            List<SaleReportItem> reportItems = new List<SaleReportItem>();

            // Loop through the filtered sales and flatten each sale into individual SaleReportItem objects
            foreach (Sale sale in filteredSales)
            {
                foreach (Product product in sale.Products)
                {
                    // Format the sale date
                    string formattedSaleDate = DateTime.Parse(sale.SaleDate).ToString("yyyy-MM-dd HH:mm:ss");

                    // Add the sale data as a new SaleReportItem to the reportItems list
                    reportItems.Add(new SaleReportItem
                    {
                        SaleId = sale.SaleId,              // Assign SaleId
                        SaleDate = formattedSaleDate,      // Assign formatted SaleDate
                        Product = product.ProductName,     // Assign product name
                        Total = sale.Total                 // Assign sale total
                    });
                }
            }

            // Bind the flattened data to the DataGrid for display
            SalesDataGrid.ItemsSource = reportItems;

            // Calculate the total sales amount by summing the sale totals
            decimal totalSales = filteredSales.Sum(sale => sale.Total);

            // Display the total sales value as currency in the TotalSalesText
            TotalSalesText.Text = totalSales.ToString("C");
        }
        
        /// <summary>
        /// Handles the click event for saving the report to a file.
        /// Allows the user to save the sales report as a CSV file.
        /// </summary>
        private void SaveReportButton_Click(object sender, RoutedEventArgs e)
        {
            // Prompt the user for a file path using the SaveFileDialog
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*", // Filter for CSV and all file types
                FileName = "SalesReport.csv"  // Default file name for the saved report
            };

            // Show the save file dialog and check if the user selected a file
            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                string filePath = saveFileDialog.FileName;  // Get the selected file path

                // Use CsvHelper library to write the report data to a CSV file
                using (var writer = new System.IO.StreamWriter(filePath))  // Open the file for writing
                using (var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture))  // Initialize CsvWriter
                {
                    // Write the header row for the CSV file (column names)
                    csv.WriteHeader<SaleReportItem>();
                    csv.NextRecord();  // Move to the next record after the header

                    // Write the data records (one for each sale report item)
                    foreach (SaleReportItem item in SalesDataGrid.ItemsSource)
                    {
                        csv.WriteRecord(item);  // Write the current item to the CSV file
                        csv.NextRecord();       // Move to the next record
                    }
                }

                // Inform the user that the report has been saved successfully
                MessageBox.Show("Report saved successfully!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        
        /// <summary>
        /// Handles the click event for closing the SalesReport window.
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();  // Close the current window
        }
    }
}

