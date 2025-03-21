using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json; // For System.Text.Json


namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Product> _products;
        
        public MainWindow()
        {
            InitializeComponent();
            //MainContent.Content = new Login(); 
            SetDate();
            SetTime();

        }

        private void SetDate()
        {
            // Fetch the current date and time
            DateTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");  //For modification not fetching real date
            
        }
        private void SetTime()
        {
            // Fetch the current date and time
            TimeTextBox.Text = DateTime.Now.ToString("hh:mm tt"); //For modification not fetching real time
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the OpeningFund window
            Search searchWindow = new Search();

            // Show the OpeningFund window
            searchWindow.ShowDialog();
        }

        //Trigger the EndofDay Window
        private void EndofDayButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the EndofDay window
            EndofDay EndofDayWindow = new EndofDay();

            // Show the EndofDay window
            EndofDayWindow.ShowDialog();
        }
    }
}
