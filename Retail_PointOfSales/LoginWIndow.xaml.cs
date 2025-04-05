using System.Windows;

namespace Retail_PointOfSales
{
    /// <summary>
    /// Interaction logic for LoginWIndow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the login button click event by verifying user credentials and opening the main window if successful.
        /// </summary>
        /// <param name="sender">The source of the event, typically the login button.</param>
        /// <param name="e">Event arguments containing event-related data.</param>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text; // UserName entered by the user
            string password = PasswordBox.Password; // Password entered by the user
            
            // if username or password are empty, show a message.
            if (username == "" || password == "")
            {
                MessageBox.Show("Username and password are required", "Login Failed", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            UserManager userManager = new UserManager(); // Create a new instance of the UserManager class
            
            // Create a new instance of the user class passing the userManager.LoginUser to validate the credentials.
            User user = userManager.LoginUser(username, password); 

            // If a user is returned, then create a new instance of MainWindow.
            if (user != null)
            {
                MainWindow mainWindow = new MainWindow(); // MainWindow new instance
                mainWindow.Show(); // Open MainWindow

                Close(); // Close LoginWindow
            }
            else
            {
                // If no user is returned, then show an error message.
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
