using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Retail_PointOfSales.Model
{
    public class EndOfDay
    {
        // Define the file path where the JSON data will be saved. The file is located relative to the application's base directory.
        // The Path.Combine method ensures correct path formation for different environments.
        string filePath = Path.Combine(AppContext.BaseDirectory, @"..\..\..\JSON\endOfDay.json");

        public string Date { get; set; } // Property to store the date when the EndOfDay is recorded.

        // The following properties are used to store the summary values in EndOfDay Window
        public decimal OpeningFundEd { get; set; } // Property to store the total opening fund value
        public decimal CashSalesEd { get; set; } // Property to store the total cash sales value
        public decimal ExpectedCashEd { get; set;  } // Property to store the expected cash value
        public decimal TotalCashCountEd { get; set; } // Property to store the total cash in drawer value
        public decimal VarianceEd { get; set; } // Property to store the total cash in drawer value
        public decimal CreditCardSalesEd { get; set; } // Property to store the total credit card sales value
        public decimal TotalSalesEd { get; set; } // Property to store the total sales value

        List<EndOfDay> endOfDay = new();

        /// <summary>
        /// Saves the EndOfDay data to a JSON file at the specified file path.
        /// This method serializes the provided EndOfDay object into a JSON formatted string
        /// and writes it to a file so it can be stored and used later.
        /// </summary>
        /// <param name="endOfDayData">The OpeningFunds object that contains the data to be saved.</param>
        public void SaveEndOfDay(EndOfDay endOfDayData)
        {
            endOfDay.Add(endOfDayData);
            // Create a StreamWriter to write to the file specified by filePath.
            // Using 'using' ensures that the file will be properly closed after writing.
            using StreamWriter fileToWrite = new StreamWriter(filePath);

            // Create a JsonSerializer instance to handle the serialization of the object to JSON.
            JsonSerializer serializer = new JsonSerializer();

            // Serialize the OpeningFunds object (fundData) and write it to the file.
            // This converts the OpeningFunds object into a JSON formatted string and saves it in the file.
            serializer.Serialize(fileToWrite, endOfDay);
        }

    }
}
