using System.Windows;
using System.Windows.Controls;

namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// This class represents the main window of the Retail Point of Sale system.
    /// It handles user interactions, including updating the date and time, searching for products,
    /// adding products to the cart, and managing the sale process.
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Product> _products;
        // Initializing the actual date and time and setting it to the actualDate variable
        DateTime actualDate = DateTime.Now;
     
        private Product selectedProduct;
        ProductManager productManager = new ();
        
        /// <summary>
        /// Initializes the MainWindow.
        /// This constructor initializes the components and sets up a timer that updates the time every second.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            // MainContent.Content = new Login(); 

            // Initializes a new instance of the Timer class that raises an event every 1000 milliseconds (1 second)
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += dispatcherTimer_Tick; // Setting event handler for the timer interval
            timer.Start(); // Starting the timer
        }

        /// <summary>
        /// Event handler for the timer interval.
        /// Updates the actual date and time and sets the DateTextBox and TimeTextBox in the UI.
        /// </summary>
        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            actualDate = DateTime.Now;
            // Updating the Date and Time in the UI - Putting it in the Dispatcher thread that runs in the UI

            if (Application.Current != null && DateTextBox != null && TimeTextBox != null) // This line prevents NullReferenceExeption when the window is closed
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    DateTextBox.Text = actualDate.ToString("MM/dd/yyyy");
                    TimeTextBox.Text = actualDate.ToString("HH:mm:ss");
                });
            }
                
        }

        /// <summary>
        /// Event handler for the TextChanged event of the Search TextBox.
        /// Placeholder functionality for implementing a search feature using a combo box.
        /// </summary>
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Just placeholder functionality if we want a combo box search
            // We can omit this if we don't have enough time
        }

        /// <summary>
        /// Event handler for the Opening Fund Button click.
        /// Opens a new window to handle the opening fund process.
        /// </summary>
        private void OpeningFundButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the OpeningFund window
            OpeningFund openingFundWindow = new OpeningFund();

            // Show the OpeningFund window
            openingFundWindow.ShowDialog();
        }

        /// <summary>
        /// Opens the search panel and loads all products into the search list.
        /// </summary>
        private void OpenSearchPanel(object sender, RoutedEventArgs e)
        {
            SearchTextBoxProductsPanel.Text = ""; //Clears the text on search box if any
            SearchPanel.Visibility = Visibility.Visible;
            SearchPanel_ProductList.ItemsSource = productManager.LoadAllProducts(); // Load Products
        }

        /// <summary>
        /// Closes the search panel.
        /// </summary>
        private void CloseSearchPanel(object sender, RoutedEventArgs e)
        {
            SearchPanel.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Event handler for the product selection event in the search panel.
        /// Selects a product and opens the quantity input panel.
        /// </summary>
        private void ProductSelected(object sender, SelectionChangedEventArgs e)
        {
            // if the selected product in the list is of type Product
            if (SearchPanel_ProductList.SelectedItem is Product product)
            {
                selectedProduct = product; // Set the selected product information
                SearchPanel.Visibility = Visibility.Collapsed; // Hide the search panel
                OpenQuantityPanel(); // Open the quantity panel
            }
        }

        /// <summary>
        /// Opens the quantity input panel to allow the user to input the quantity of the selected product.
        /// </summary>
        private void OpenQuantityPanel()
        {
            // If no product is selected, prevent opening the panel
            if (selectedProduct == null) return;
            // Set the product name to the selected product text
            SelectedProductText.Text = $"Enter quantity for {selectedProduct.ProductName}:";
            QuantityPanel.Visibility = Visibility.Visible; // Show the quantity panel
        }

        /// <summary>
        /// Closes the quantity input panel.
        /// </summary>
        private void CloseQuantityPanel(object sender, RoutedEventArgs e)
        {
            QuantityPanel.Visibility = Visibility.Collapsed; // Hide the quantity panel
        }

        /// <summary>
        /// Confirms the quantity entered by the user and adds the selected product to the cart.
        /// </summary>
        private void ConfirmQuantity(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(QuantityTextBox.Text, out int quantity) && quantity > 0)
            {
                AddProductToCart(selectedProduct, quantity); // Add product to the cart
                QuantityPanel.Visibility = Visibility.Collapsed; // Close the quantity panel
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity."); // Show an error message
            }
        }

        /// <summary>
        /// Adds the selected product to the cart with the specified quantity.
        /// </summary>
        private void AddProductToCart(Product product, int quantity)
        {
            Product productToAdd = new Product
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                Quantity = quantity,
            };
            QuantityTextBox.Text = ""; // Clear the quantity textbox
            ProductListView.Items.Add(productToAdd); // Add product to the list view
            UpdateSaleTotal(); // Update the sale total
        }

        /// <summary>
        /// Searches for products based on the search input and displays matching products.
        /// </summary>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBoxProductsPanel.Text.ToLower(); // Get the search text from user input
            var result = productManager.Search(searchText); // Search for matching products
            if (result != null)
            {
                // If products are found, display them in the search panel
                SearchPanel_ProductList.ItemsSource = result;
            }
            else
            {
                // If no products are found, show an error message
                MessageBox.Show("Product not found. Please try again.", "Product not found", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Opens the End of Day window.
        /// </summary>
        private void EndofDayButton_Click(object sender, RoutedEventArgs e)
        {
            EndofDay EndofDayWindow = new EndofDay();
            EndofDayWindow.ShowDialog(); // Show the EndofDay window
        }
        
        /// <summary>
        /// Deletes the selected product from the cart.
        /// </summary>
        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(ProductListView);
            if (ProductListView.Items.Count == 0)
            {
                MessageBox.Show("No products to delete!");
                return;
            }
            else if (ProductListView.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }
            else
            {
                int productToRemove = ProductListView.SelectedIndex; // Get the selected product index
                if (productToRemove.Equals(-1)) return; // Prevents exceptions when clicking delete button with no product selected
                ProductListView.Items.RemoveAt(productToRemove); // Remove the product from the list
                UpdateSaleTotal(); // Update the sale total
            }
        }

        /// <summary>
        /// Updates the total sale amount by summing up the prices of the items in the cart.
        /// </summary>
        private void UpdateSaleTotal()
        {
            decimal total = 0;
            foreach (Product item in ProductListView.Items)
            {
                total += item.Amount; // Sum the product prices
            }
            
            TotalTextBlock.Text = total.ToString("0.00"); // Update the total display with the formatted value
        }

        private void CashPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            List<Product> products = new List<Product>();
            
            foreach (var item in ProductListView.Items)
            {
                if (item is Product product)
                {
                    products.Add(product);
                }
            }
            
            Sale sale = new Sale
            {
                Products = products,
                PaymentMethod = PaymentMethod.Cash.ToString(),
                SaleId = Guid.NewGuid().ToString(),
                Subtotal = decimal.Parse(TotalTextBlock.Text),
                Total = decimal.Parse(TotalTextBlock.Text)
            };

            CashPayment CashPaymentWindow = new CashPayment(sale);
            // Show the CashPayment window
            CashPaymentWindow.ShowDialog();
            if (CashPaymentWindow.DialogResult == true)
            {
                MessageBox.Show(
                    "Thank you for your purchase with us.", 
                    "Purchase completed", 
                    MessageBoxButton.OK, MessageBoxImage.Information
                    );
                ProductListView.Items.Clear();
                TotalTextBlock.Text = "";
            }
        }

        private void SalesReportButton_Click(object sender, RoutedEventArgs e)
        {
            
            SalesReport SalesReportWindow = new SalesReport();

            // Show the SalesReport window
            SalesReportWindow.ShowDialog();
        }

        private void CreditPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            List<Product> products = new List<Product>();
            
            foreach (var item in ProductListView.Items)
            {
                if (item is Product product)
                {
                    products.Add(product);
                }
            }
            
            Sale sale = new Sale
            {
                Products = products,
                PaymentMethod = PaymentMethod.CreditCard.ToString(),
                SaleId = Guid.NewGuid().ToString(),
                Subtotal = decimal.Parse(TotalTextBlock.Text),
                Total = decimal.Parse(TotalTextBlock.Text),
            };
            
            CreditPayment CreditPaymentWindow = new CreditPayment(sale);
            // Show the CreditPayment window
            CreditPaymentWindow.ShowDialog();
            if (CreditPaymentWindow.DialogResult == true)
            {
                MessageBox.Show(
                    "Thank you for your purchase with us.", 
                    "Purchase completed", 
                    MessageBoxButton.OK, MessageBoxImage.Information
                );
                ProductListView.Items.Clear();
                TotalTextBlock.Text = "";
            }
        }

        // Exits the application in WPF
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation",
            MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown(); 
            }
        }

        /// <summary>
        /// Clears all the products in the cart and updates the value of sale total.
        /// </summary>
        private void VoidOrder_Click(object sender, RoutedEventArgs e)
        {
            // Checks if the cart is empty
            if (ProductListView.Items.Count == 0)
            {
                // Show a warning message if the cart is empty
                MessageBox.Show("The order is empty!", "Warning");
            }
            else
            {
                //Validate if the user really wants to void the order
                MessageBoxResult result = MessageBox.Show("Are you sure you want to void the order?", "Void order", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result != MessageBoxResult.No)
                {
                    ProductListView.Items.Clear(); // Clear the product list
                    UpdateSaleTotal(); // Update the sale total
                }
            }
        }

    }
}
