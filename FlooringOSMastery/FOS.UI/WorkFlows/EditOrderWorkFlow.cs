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
    public class EditOrderWorkFlow
    {
        public void Execute(OrderManager manager)
        {
            Console.Clear();
            List<Tax> taxList = manager.RetrieveAllTax();
            List<Product> productList = manager.RetrieveAllProducts();

            Console.Clear();
            Console.WriteLine("Edit an Order");
            Console.WriteLine(ConsoleIO.SeparatorBar);
            Console.WriteLine("");

            int orderNumber = ConsoleIO.GetRequiredIntFromUser("Enter an order number: ");
            DateTime orderDate = ConsoleIO.GetRequiredLookUpDateTimeFromUser("Enter order date: ");

            OrderLookupResponse lookupResponse = manager.LookupOrder(orderDate, orderNumber); // CONFIRM ORDER EXISTS


            if (lookupResponse.Success)                // PROCEED TO EDIT
            {
                int changesMade = 0;

                ConsoleIO.DisplayOrderDetails(lookupResponse.Order); // DISPLAY UNEDITED VERSION OF THE ORDER
                Console.WriteLine("\n Enter new order details, or press enter to leave unchanged.");

                Console.Write($"Enter Customer Name ({lookupResponse.Order.CustomerName}):"); // USER INPUT CUSTOMERNAME OR ENTER
                string editCustomerName = Console.ReadLine();
                if (!string.IsNullOrEmpty(editCustomerName)) // IF THEY DID NOT LEAVE IT BLANK, 
                {
                    lookupResponse.Order.CustomerName = editCustomerName; // UPDATE IT TO WHATEVER THEY PUT IN.
                }

                ConsoleIO.DisplayStateList(taxList); // DISPLAY LIST OF STATES
                Console.Write($"State: ({lookupResponse.Order.State}):"); // USER INPUT STATE OR ENTER
                while (true)
                {
                    string editState = Console.ReadLine();

                    if (string.IsNullOrEmpty(editState))
                    {
                        editState = lookupResponse.Order.State;
                        break;
                    }
                    else if (taxList.Where(tax => tax.StateAbbreviation == editState.ToUpper()).ToList().Count == 0)
                    {
                        Console.WriteLine($"{editState} is not a state serviced by this company. Please provide a valid state.");
                        editState = Console.ReadLine();
                    }
                    else
                    {
                        lookupResponse.Order.State = editState;
                        changesMade++;
                        break;
                    }

                }
                ConsoleIO.DisplayProductList(productList); // DISPLAY LIST OF PRODUCTS
                Console.Write($"Product Type: ({lookupResponse.Order.ProductType}):"); // USER INPUT PRODUCT OR ENTER

                while (true)
                {
                    string editProductType = Console.ReadLine();

                    if (string.IsNullOrEmpty(editProductType))
                    {
                        editProductType = lookupResponse.Order.ProductType;
                        break;
                    }
                    else if (productList.Where(product => product.ProductType == editProductType).ToList().Count == 0)
                    {
                        Console.WriteLine($"{editProductType} is not a product provided by this company. Please provide a valid product type.");
                        editProductType = Console.ReadLine();
                    }
                    else
                    {
                        lookupResponse.Order.ProductType = editProductType;
                        changesMade++;
                        break;
                    }

                }

                Console.Write($"Area: ({lookupResponse.Order.Area}):"); // USER INPUT AREA AS STRING
                string editAreaInput = Console.ReadLine();
                decimal editArea;                                     // AREA NEEDS TO BE A DECIMAL SO DECLARED HERE FOR LATER

                while (true)
                {
                    if (string.IsNullOrEmpty(editAreaInput)) // IF THE AREA STRING IS BLANK
                    {
                        editArea = lookupResponse.Order.Area; // REMAINS THE SAME
                        break;
                    }
                    else if (!decimal.TryParse(editAreaInput, out editArea)) // CHECK TO MAKE SURE THE STRING THEY ENTERED CAN BE PARSED TO DECIMAL
                    {                                                   // IF IT CAN'T, TELL THEM TO LEAVE IT UNCHANGED OR GIVE A VALID DECIMAL
                        Console.WriteLine("You must enter a valid decimal, or hit enter to remain unchanged.");
                        editAreaInput = Console.ReadLine();
                    }
                    else if (editArea < 100) // IF THEY'VE PROVIDED A VALID DECIMAL TO CHANGE IT TO, CHECK IT'S OVER 100.
                    {
                        Console.WriteLine("You must enter an area greater than or equal to 100.");
                        editAreaInput = Console.ReadLine();
                    }
                    else
                    {
                        lookupResponse.Order.Area = editArea;
                        changesMade++;
                        break;
                    }

                }

                if (changesMade == 0)
                {
                    // CONFIRM USER WANTS TO UPDATE ORDER NAME
                    Console.Clear();
                    Console.WriteLine("Order Summary");
                    Console.WriteLine(ConsoleIO.SeparatorBar);
                    ConsoleIO.DisplayOrderDetails(lookupResponse.Order); // DISPLAY ORDER AFTER NAME UPDATED

                    if (ConsoleIO.GetYesNoAnswerFromUser("Save updated order?") == "Y")
                    {
                        AddOrderResponse addResponse = new AddOrderResponse(); // BC THEY WANT TO SAVE, CREATE AN AddResponse
                        addResponse = manager.AddOrder(lookupResponse.Order); // addResponse will be the response I get back by trying to save response.Order

                        if (addResponse.Success)
                        {
                            Console.Clear();
                            Console.WriteLine("Order Updated Successfully!");
                            ConsoleIO.DisplayOrderDetails(addResponse.Order);
                        }
                        else
                        {
                            Console.WriteLine("An error occurred: ");
                            Console.WriteLine(addResponse.Message);
                        }
                    }
                    else // THEY DON'T WANT TO SAVE, DISCARD IT RETURN TO MAIN MENU
                    {
                        Console.WriteLine("Changes Cancelled!");

                    }
                }
                else if (changesMade != 0)
                {
                    // PASS UPDATED ORDER INFO TO CALCULATOR ONLY IF THE USER MADE CHANGES TO STATE/PRODUCT/AREA
                    CalculateOrderResponse calculateResponse = new CalculateOrderResponse();
                    calculateResponse = manager.CalculateOrder(lookupResponse.Order); // OVERWRITE response.Order W/CALCULATIONS

                    if (calculateResponse.Success)
                    {

                        Console.Clear();
                        Console.WriteLine("Order Summary");
                        Console.WriteLine(ConsoleIO.SeparatorBar);
                        ConsoleIO.DisplayOrderDetails(calculateResponse.Order); // DISPLAY ORDER AFTER CALCULATING

                        if (ConsoleIO.GetYesNoAnswerFromUser("Save updated order?") == "Y")
                        {
                            AddOrderResponse addResponse = new AddOrderResponse(); // BC THEY WANT TO SAVE, CREATE AN AddResponse
                            addResponse = manager.AddOrder(calculateResponse.Order); // addResponse will be the response I get back by trying to save response.Order

                            if (addResponse.Success)
                            {
                                Console.Clear();
                                Console.WriteLine("Order Updated Successfully!");
                                ConsoleIO.DisplayOrderDetails(addResponse.Order);
                            }
                            else
                            {
                                Console.WriteLine("An error occurred: ");
                                Console.WriteLine(addResponse.Message);
                            }
                        }
                        else // THEY DON'T WANT TO SAVE, DISCARD IT RETURN TO MAIN MENU
                        {
                            Console.WriteLine("Changes Cancelled!");
                        }

                    }
                    else
                    {
                        Console.WriteLine("An error occurred: "); // INVALID NAME/STATE/PRODUCT/AREA
                        Console.WriteLine(calculateResponse.Message);
                    }
                }
                else
                {
                    Console.WriteLine("An error occurred: "); // ORDER DOES NOT EXIST
                    Console.WriteLine(lookupResponse.Message);
                }

                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }
    }
}


