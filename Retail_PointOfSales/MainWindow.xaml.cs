using System.Windows;
using System.Windows.Controls;

namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Product> _products;
        //Initializing the actual date and time and setting it to the actualDate variable
        DateTime actualDate = DateTime.Now;
     
        private Product selectedProduct;
        ProductManager productManager = new ();
        
        public MainWindow()
        {
            InitializeComponent();
            //MainContent.Content = new Login(); 

            // Initializes a new instance of the Timer class that raises an event at every 1000 milliseconds (1 second)
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += dispatcherTimer_Tick; //Setting event handler for the timer interval
            timer.Start(); // Startig the timer
        }

        // Event handler for the timer interval
        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            actualDate = DateTime.Now;
            // Updating the Date and Time in the UI - Putting it in the Dispatcher thread that runs in the UI
            Application.Current.Dispatcher.Invoke(() =>
            {
                DateTextBox.Text = actualDate.ToString("MM/dd/yyyy");
                TimeTextBox.Text = actualDate.ToString("HH:mm:ss");
            });
        }

        // Search Text Box from the JSON File
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Just placeholder functionality if we want a combo box search
            // We can omit this if we don't have enough time
        }

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Just placeholder functionality if we want a combo box search
            // We can omit this if we don't have enough time
        }

        private void OpeningFundButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the OpeningFund window
            OpeningFund openingFundWindow = new OpeningFund();

            // Show the OpeningFund window
            openingFundWindow.ShowDialog();
        }
        
        private void OpenSearchPanel(object sender, RoutedEventArgs e)
        {
            SearchPanel.Visibility = Visibility.Visible;
            SearchPanel_ProductList.ItemsSource = productManager.LoadAllProducts(); // Load Products
        }
        
        // Close Search Panel
        private void CloseSearchPanel(object sender, RoutedEventArgs e)
        {
            SearchPanel.Visibility = Visibility.Collapsed;
        }
        
        
        private void ProductSelected(object sender, SelectionChangedEventArgs e)
        {
            if (SearchPanel_ProductList.SelectedItem is Product product)
            {
                selectedProduct = product;
                SearchPanel.Visibility = Visibility.Collapsed; // Hide search panel
                OpenQuantityPanel();
            }
        }
        
        // Open Quantity Panel
        private void OpenQuantityPanel()
        {
            if (selectedProduct == null) return;
            SelectedProductText.Text = $"Enter quantity for {selectedProduct.ProductName}:";
            QuantityPanel.Visibility = Visibility.Visible;
        }
        
        // Close Quantity Panel
        private void CloseQuantityPanel(object sender, RoutedEventArgs e)
        {
            QuantityPanel.Visibility = Visibility.Collapsed;
        }
        
        // Confirm Quantity and Add to Cart
        private void ConfirmQuantity(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(QuantityTextBox.Text, out int quantity) && quantity > 0)
            {
                AddProductToCart(selectedProduct, quantity);
                QuantityPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity.");
            }
        }
        
        // Add Product to Cart
        private void AddProductToCart(Product product, int quantity)
        {
            decimal total = 0;
            Product productToAdd = new Product
            {
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                Quantity = quantity,
            };
            
            ProductListView.Items.Add(productToAdd);
            
            foreach (Product item in ProductListView.Items)
            {
                total += item.Amount;
            }
            
            TotalTextBlock.Text = $"{total:C}";
        }
        
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox2.Text.ToLower(); // text input by user
            // call search function from ProductManager class passing the text as argument to search for it.
            var result= productManager.Search(searchText);
            if (result != null)
            {
                // if not null, then the list will render all products that matches with the searchText
                SearchPanel_ProductList.ItemsSource = result;
            }
            else
            {
                // Otherwise an error message will be displayed
                MessageBox.Show("Product not found. Please try again.", "Product not found.", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
