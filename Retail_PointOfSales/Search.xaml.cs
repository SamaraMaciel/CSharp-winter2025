using System.Windows;

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
            // products
        }
    }
}
