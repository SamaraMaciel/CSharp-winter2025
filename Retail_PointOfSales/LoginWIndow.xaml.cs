using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for LoginWIndow.xaml
    /// </summary>
    public partial class LoginWIndow : Window
    {
        public LoginWIndow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // User Login
            if (username == "admin" && password == "password") 
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show(); // Open MainWindow

                this.Close(); // Close LoginWindow
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
