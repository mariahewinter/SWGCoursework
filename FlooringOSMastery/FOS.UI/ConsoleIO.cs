using FOS.Models;
using FOS.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.UI
{
    public class ConsoleIO
    {
        public const string SeparatorBar = "**********************************";
        public static void DisplayOrderDetails(Order order)
        {
            Console.WriteLine("**********************************");
            Console.WriteLine($"{order.OrderNumber} || {order.OrderDate.ToString("MM/dd/yyyy")}");
            Console.WriteLine($"{order.CustomerName}");
            Console.WriteLine($"{order.State}");
            Console.WriteLine($"Product: {order.ProductType}");
            Console.WriteLine($"Materials: {order.MaterialCost:c}");
            Console.WriteLine($"Labor: {order.LaborCost:c}");
            Console.WriteLine($"Tax: {order.Tax:c}");
            Console.WriteLine($"Total: {order.Total:c}");
            Console.WriteLine("**********************************\n");
        }

        public static void DisplayStateList(List<Tax> taxList)
        {
            foreach (Tax tax in taxList)
            {
                Console.WriteLine("\n**********************************");
                Console.WriteLine(tax.StateName + "|" + tax.StateAbbreviation);
                Console.WriteLine("**********************************\n");
            }
        }
        public static void DisplayProductList(List<Product> productList)
        {
            foreach (Product product in productList)
            {
                Console.WriteLine("\n**********************************");
                Console.WriteLine($"{product.ProductType} | { product.CostPerSquareFoot:c} | {product.LaborCostPerSquareFoot:c}");
                Console.WriteLine("**********************************\n");
            }
        }
        public static string GetYesNoAnswerFromUser(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + "Y/N? ");
                string input = Console.ReadLine().ToUpper();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must enter Y/N.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (input != "Y" && input != "N")
                    {
                        Console.WriteLine("You must enter Y/N.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    return input;
                }
            }
        }
        public static int GetRequiredIntFromUser(string prompt)
        {
            int output;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out output))
                {
                    Console.WriteLine("You must enter a valid number.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
                else
                {
                    return output;
                }
            }
        }

        public static DateTime GetRequiredLookUpDateTimeFromUser(string prompt)
        {
            DateTime output;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!DateTime.TryParse(input, out output))
                {
                    Console.WriteLine("{0} is not a valid date", input);
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
                else
                {
                    return output;
                }
            }

        }

        public static DateTime GetRequiredFutureDateTimeFromUser(string prompt)
        {
            DateTime output;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!DateTime.TryParse(input, out output))
                {
                    Console.WriteLine("{0} is not a valid date", input);
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
                else if (output <= DateTime.Today)
                {

                    Console.WriteLine("Order date must be in the future.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
                else
                {
                    return output;
                }

            }

        }

        public static string GetRequiredStringFromUser(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input)) // check if user entered nothing or blank spaces
                {
                    Console.WriteLine("You must enter valid text.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }

            }
        }

        public static string GetRequiredProductFromUser(string prompt, List<Product> productList)
        {

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input)) // check if user entered nothing or blank spaces
                {
                    Console.WriteLine("You must enter valid text.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
                else if (!productList.Exists(product => product.ProductType == input)) // if a product.ProductType equal to userinput doesn't exist in the product list...
                {
                    Console.WriteLine("You must enter a valid product.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }

        public static string GetRequiredStateFromUser(string prompt, List<Tax> taxList)
        {

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input)) // check if user entered nothing or blank spaces
                {
                    Console.WriteLine("You must enter valid text.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
                else if (!taxList.Exists(tax => tax.StateAbbreviation == input.ToUpper())) // check if user entered a state abbreviation that isn't on our list
                {
                    Console.WriteLine("You must enter a valid state.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }
        public static decimal GetRequiredDecimalFromUser(string prompt)
        {
            decimal output;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!decimal.TryParse(input, out output))
                {
                    Console.WriteLine("You must enter a valid decimal.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
                else
                {
                    if (output <= 0)
                    {
                        Console.WriteLine("You must enter a positive decimal.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                    }
                    return output;
                }
            }
        }

        public static decimal GetRequiredAreaFromUser(string prompt)
        {
            decimal output;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!decimal.TryParse(input, out output))
                {
                    Console.WriteLine("You must enter a valid decimal.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
                else if (output < 100)
                {

                    Console.WriteLine("You must enter an area greater than or equal to 100.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();

                }
                else
                {
                    return output;
                }

            }
        }


    }
}
