using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_PointOfSales
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; } = 0; // Default to 0
        public decimal Amount => Quantity * ProductPrice; // Auto-calculated
    }
}
