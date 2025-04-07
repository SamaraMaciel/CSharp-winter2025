using System.Windows;
using System.Windows.Controls;
using Retail_PointOfSales.Model;

namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// This class represents the main window of the Retail Point of Sale system.
    /// It handles user interactions, including updating the date and time, searching for products,
    /// adding products to the cart, and managing the sale process.
    /// </summary>
    public partial class MainWindow
    {
        // Initializing the actual date and time and setting it to the actualDate variable
        DateTime actualDate = DateTime.Now;
     
        private Product selectedProduct;
        private readonly ProductManager productManager = new ();
        
        /// <summary>
        /// Initializes the MainWindow.
        /// This constructor initializes the components and sets up a timer that updates the time every second.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

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
            SearchTextBoxProductsPanel.Text = ""; // Clears the text on the search box if any
            SearchPanel.Visibility = Visibility.Visible;
            SearchPanel_ProductList.ItemsSource = productManager.LoadAllProducts(); // Load Products
        }

        /// <summary>
        /// Closes the search panel.
        /// </summary>
        private void CloseSearchPanel(object sender, RoutedEventArgs e)
        {
            // Set the panel's visibility to collapsed when clicking close button
            SearchPanel.Visibility = Visibility.Collapsed;
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
            // Attempt to parse the text entered in the QuantityTextBox as an integer
            if (int.TryParse(QuantityTextBox.Text, out int quantity) && quantity > 0)
            {
                // If parsing succeeds and the quantity is greater than 0, add the product to the cart
                AddProductToCart(selectedProduct, quantity); // Add the selected product to the cart with the specified quantity
        
                // Hide the quantity selection panel after confirming the quantity
                QuantityPanel.Visibility = Visibility.Collapsed; // Close the panel where the user selected the quantity
            }
            else
            {
                // If the entered quantity is invalid (not a number or less than 1), show an error message
                MessageBox.Show("Please enter a valid quantity."); // Display an error message to the user
            }
        }

        /// <summary>
        /// Adds the selected product to the cart with the specified quantity.
        /// </summary>
        private void AddProductToCart(Product product, int quantity)
        {
            // Check if the product already exists in the cart
            var existingProduct = ProductListView.Items.Cast<Product>()
                .FirstOrDefault(p => p.ProductId == product.ProductId);
    
            if (existingProduct != null)
            {
                // If the product is found, update the quantity and price
                existingProduct.Quantity += quantity;
                
                // Remove the old product and add the updated one
                ProductListView.Items.Remove(existingProduct);
                ProductListView.Items.Add(existingProduct);
            }
            else
            {
                // If the product is not found, create a new product and add it to the list
                Product productToAdd = new Product
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice * quantity,
                    Quantity = quantity,
                };

                ProductListView.Items.Add(productToAdd); // Add the new product to the list view
            }

            QuantityTextBox.Text = ""; // Clear the quantity textbox
            UpdateSaleTotal(); // Update the sale total
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
            // Check if there are no products in the cart (empty list)
            if (ProductListView.Items.Count == 0)
            {
                // Show a message if the cart is empty
                MessageBox.Show("No products to delete!");
            }
            // Check if no product is selected (no item in ProductListView is highlighted)
            else if (ProductListView.SelectedIndex.Equals(-1))
            {
                // Show a message if no product is selected for deletion
                MessageBox.Show("Please select a product to delete.");
            }
            else
            {
                // Get the index of the selected product in the ProductListView
                int productToRemove = ProductListView.SelectedIndex; 
        
                // Prevents the method from running if the selected index is invalid (-1)
                if (productToRemove.Equals(-1)) return; 
        
                // Remove the selected product from the ProductListView
                ProductListView.Items.RemoveAt(productToRemove); 
        
                // Update the sale total after deleting a product
                UpdateSaleTotal(); 
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

        /// <summary>
        /// Opens the Cash Payment window for completing the sale.
        /// </summary>
       private void CashPaymentButton_Click(object sender, RoutedEventArgs e)
        { 
            // Check if there are no items in the cart (empty order)
            if (ProductListView.Items.Count == 0)
            {
                // Show a warning message if the cart is empty
                MessageBox.Show("The order is empty!", "Empty order",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Exit the method if the cart is empty
            }
    
            // Create a list to store the products in the cart
            List<Product> products = new List<Product>();
    
            // Loop through each item in the ProductListView (the cart)
            foreach (var item in ProductListView.Items)
            {
                // If the item is of type Product, add it to the products list
                if (item is Product product)
                {
                    products.Add(product);
                }
            }
    
            // Create a new Sale object to represent the sale details
            Sale sale = new Sale
            {
                Products = products, // Assign the list of products to the sale
                PaymentMethod = PaymentMethod.Cash.ToString(), // Set the payment method to "Cash"
                SaleId = Guid.NewGuid().ToString(), // Generate a new unique Sale ID
                Subtotal = decimal.Parse(TotalTextBlock.Text), // Parse the subtotal from the total text block
                Total = decimal.Parse(TotalTextBlock.Text), // Set the total to the same as the subtotal for now
            };

            // Create a new instance of the CashPayment window, passing in the sale details
            CashPayment CashPaymentWindow = new CashPayment(sale);
    
            // Show the CashPayment window as a modal dialog
            CashPaymentWindow.ShowDialog();
    
            // If the user confirms the payment (DialogResult is true)
            if (CashPaymentWindow.DialogResult == true)
            {
                // Show a confirmation message to the user
                MessageBox.Show(
                    "Thank you for your purchase with us.", 
                    "Purchase completed", 
                    MessageBoxButton.OK, MessageBoxImage.Information
                );
        
                // Clear the cart (ProductListView) and reset the total display
                ProductListView.Items.Clear();
                TotalTextBlock.Text = "";
            }
        }

        /// <summary>
        /// Opens the Credit Payment window for completing the sale.
        /// </summary>
        private void CreditPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if there are no items in the cart (empty order)
            if (ProductListView.Items.Count == 0)
            {
                // Show a warning message if the cart is empty
                MessageBox.Show("The order is empty!", "Empty order",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Exit the method if the cart is empty
            }
    
            // Create a list to store the products in the cart
            List<Product> products = new List<Product>();
    
            // Loop through each item in the ProductListView (the cart)
            foreach (var item in ProductListView.Items)
            {
                // If the item is of type Product, add it to the products list
                if (item is Product product)
                {
                    products.Add(product);
                }
            }
    
            // Create a new Sale object to represent the sale details
            Sale sale = new Sale
            {
                Products = products, // Assign the list of products to the sale
                PaymentMethod = PaymentMethod.CreditCard.ToString(), // Set the payment method to "CreditCard"
                SaleId = Guid.NewGuid().ToString(), // Generate a new unique Sale ID
                Subtotal = decimal.Parse(TotalTextBlock.Text), // Parse the subtotal from the total text block
                Total = decimal.Parse(TotalTextBlock.Text), // Set the total to the same as the subtotal for now
            };
    
            // Create a new instance of the CreditPayment window, passing in the sale details
            CreditPayment CreditPaymentWindow = new CreditPayment(sale);
    
            // Show the CreditPayment window as a modal dialog
            CreditPaymentWindow.ShowDialog();
    
            // If the user confirms the payment (DialogResult is true)
            if (CreditPaymentWindow.DialogResult == true)
            {
                // Show a confirmation message to the user
                MessageBox.Show(
                    "Thank you for your purchase with us.", 
                    "Purchase completed", 
                    MessageBoxButton.OK, MessageBoxImage.Information
                );
        
                // Clear the cart (ProductListView) and reset the total display
                ProductListView.Items.Clear();
                TotalTextBlock.Text = "";
            }
        }
        
        /// <summary>
        /// Opens the Sales Report window.
        /// </summary>
        private void SalesReportButton_Click(object sender, RoutedEventArgs e)
        {
            SalesReport SalesReportWindow = new SalesReport();
            // Show the SalesReport window
            SalesReportWindow.ShowDialog();
        }

        /// <summary>
        /// Exits the application in WPF
        /// </summary>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown(); // If result == Yes, close the aplication
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
                MessageBox.Show("The order is empty!", "Empty order");
            }
            else
            {
                //Validate if the user really wants to void the order
                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to void the order?", "Void order", 
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result != MessageBoxResult.No)
                {
                    ProductListView.Items.Clear(); // Clear the product list
                    UpdateSaleTotal(); // Update the sale total
                }
            }
        }
    }
}
