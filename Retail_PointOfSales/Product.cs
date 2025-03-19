using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_PointOfSales
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1; // Default to 0
        public decimal Amount => Quantity * Price; // Auto-calculated
    }
}
