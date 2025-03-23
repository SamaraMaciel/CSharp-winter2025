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
using System.ComponentModel;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the OpeningFund window
            Search searchWindow = new Search();

            // Show the OpeningFund window
            searchWindow.ShowDialog();
        }
    }
}
