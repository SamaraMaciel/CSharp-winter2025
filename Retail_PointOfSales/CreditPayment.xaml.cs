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
    /// Interaction logic for CreditPayment.xaml
    /// </summary>
    public partial class CreditPayment : Window
    {
        public CreditPayment(Sale sale)
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            //JSON File code
        }
    }
}
