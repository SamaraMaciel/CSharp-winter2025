using System.Windows;
using System.Windows.Controls;

namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// This class handles product searching and selection in the Search window.
    /// </summary>
    public partial class Search : Window
    {
        /// Manages product-related operations.
        ProductManager productManager = new ();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Search"/> class.
        /// Loads all products into the ListView.
        /// </summary>
        public Search()
        {
            InitializeComponent();
            LoadProducts();
        }

        /// <summary>
        /// Loads all products from the product manager and binds them to the ListView.
        /// </summary>
        private void LoadProducts()
        {
            ProductListView.ItemsSource = productManager.LoadAllProducts();
        }

        /// <summary>
        /// Handles the search button click event.
        /// Filters products based on the search input and updates the ListView.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data for the routed event.</param>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower(); // text input by user
            // call search function from ProductManager class passing the text as argument to search for it.
            var result= productManager.Search(searchText);
            if (result != null)
            {
                // if not null, then the list will render all products that matches with the searchText
                ProductListView.ItemsSource = result;
            }
            else
            {
                // Otherwise an error message will be displayed
                MessageBox.Show("Product not found. Please try again.", "Product not found.", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the ListView selection change event.
        /// Displays the selected product's details.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data for the selection changed event.</param>
        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If the selected product is an object of Product type
            // Then product name and price will have its text property updated with the selected item
            if (ProductListView.SelectedItem is Product selectedProduct)
            {
                ProductName.Text = selectedProduct.ProductName;
                ProductPrice.Text = selectedProduct.ProductPrice.ToString("C");
            }
        }
    }
}
