using FOS.BLL;
using FOS.Models;
using FOS.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.UI.WorkFlows
{
    public class AddOrderWorkFlow
    {
        public void Execute(OrderManager manager)
        {
            Console.Clear();
            
            Console.Clear();
            Console.WriteLine("Add an Order");
            Console.WriteLine(ConsoleIO.SeparatorBar);
            Console.WriteLine("");

            Order newOrder = new Order(); // QUERY FOR ALL PARTS OF AN ORDER OBJECT FROM USER

            newOrder.OrderDate = ConsoleIO.GetRequiredFutureDateTimeFromUser("Order Date: ");  // ORDER DATE

            newOrder.OrderNumber = 0; // Order number is 0 by default and gets set at save time

            newOrder.CustomerName = ConsoleIO.GetRequiredStringFromUser("Customer Name: "); // CUSTOMER NAME

            List<Tax> taxList = manager.RetrieveAllTax();
            ConsoleIO.DisplayStateList(taxList); // DISPLAY LIST OF STATES
            newOrder.State = ConsoleIO.GetRequiredStateFromUser("State (by abbreviation): ", taxList); // SELECT/VALIDATE STATE

            newOrder.TaxRate = 0;

            List<Product> productList = manager.RetrieveAllProducts();
            ConsoleIO.DisplayProductList(productList); // DISPLAY LIST OF PRODUCTS

            newOrder.ProductType = ConsoleIO.GetRequiredProductFromUser("Product Type: ", productList); // SELECT/VALIDATE PRODUCT TYPE

            newOrder.Area = ConsoleIO.GetRequiredAreaFromUser("Area: "); // AREA

            // CALCULATION PLACEHOLDERS FOR AFTER USER QUERY
            newOrder.CostPerSquareFoot = 0;
            newOrder.LaborCostPerSquareFoot = 0;
            newOrder.MaterialCost = 0;
            newOrder.LaborCost = 0;
            newOrder.Tax = 0;
            newOrder.Total = 0;

            AddOrderResponse addResponse = new AddOrderResponse();
            CalculateOrderResponse calculateResponse = manager.CalculateOrder(newOrder); // SEND TO BLL FOR CALCULATING

            if(calculateResponse.Success)
            {
                Console.Clear();
                Console.WriteLine("Order Summary");
                Console.WriteLine(ConsoleIO.SeparatorBar);
                ConsoleIO.DisplayOrderDetails(calculateResponse.Order); // DISPLAY ORDER AFTER CALCULATING

                if (ConsoleIO.GetYesNoAnswerFromUser("Place the order? ") == "Y") // If user says save it...
                {
                    addResponse = manager.AddOrder(calculateResponse.Order);

                    if (addResponse.Success) // Logic layer sends back a response...if it's success, great. 
                    {
                        Console.Clear();
                        Console.WriteLine("Order Saved Successfully!");
                        ConsoleIO.DisplayOrderDetails(addResponse.Order);
                    }
                    else // otherwise, it will return a message in regards to which of the 4 failed (CustomerName/State/Product/Area). 
                    {
                        Console.WriteLine("An error occurred: ");
                        Console.WriteLine(addResponse.Message);
                    }
                }
                else // if user says no don't save it...even better, just tell them it's cancelled and go on back to Main Menu.
                {
                    Console.WriteLine("Order Cancelled!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("An error occurred: ");
                Console.WriteLine(calculateResponse.Message);
                
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

        }
    }
}
