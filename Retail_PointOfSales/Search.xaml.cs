using System.Windows;
using System.Windows.Controls;

namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        ProductManager productManager = new ();
        public Search()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            ProductListView.ItemsSource = productManager.LoadAllProducts();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            var result= productManager.Search(searchText);
            if (result != null)
            {
                ProductListView.ItemsSource = result;
            }
            else
            {
                MessageBox.Show("Product not found. Please try again.", "Product not found.", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductListView.SelectedItem is Product selectedProduct)
            {
                ProductName.Text = selectedProduct.ProductName;
                ProductPrice.Text = selectedProduct.ProductPrice.ToString("C");
            }
        }
    }
}
