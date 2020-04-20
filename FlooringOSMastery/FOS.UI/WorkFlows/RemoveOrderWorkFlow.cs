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
    public class RemoveOrderWorkFlow
    {
        public void Execute(OrderManager manager)
        {
            Console.Clear();
            Console.WriteLine("Remove an order");
            Console.WriteLine(ConsoleIO.SeparatorBar);
            int orderNumber = ConsoleIO.GetRequiredIntFromUser("Enter an order number: ");
            DateTime orderDate = ConsoleIO.GetRequiredLookUpDateTimeFromUser("Enter order date: ");

            OrderLookupResponse lookupResponse = manager.LookupOrder(orderDate, orderNumber);

            if (lookupResponse.Success)
            {
                ConsoleIO.DisplayOrderDetails(lookupResponse.Order);
                if (ConsoleIO.GetYesNoAnswerFromUser("Are you sure you want to remove this order? ") == "Y")
                {
                    RemoveOrderResponse removeResponse = new RemoveOrderResponse();
                    removeResponse = manager.RemoveOrder(lookupResponse.Order, lookupResponse.Order.OrderDate); 

                    if (removeResponse.Success)
                    {
                        Console.Clear();
                        Console.WriteLine("Order Removed Successfully!");
                    }
                    else
                    {
                        Console.WriteLine("An error occurred: ");
                        Console.WriteLine(removeResponse.Message);
                    }
                }
                else // THEY DON'T WANT TO REMOVE, RETURN TO MAIN MENU
                {
                    Console.WriteLine("Removal Cancelled!");
                }
            }
            else
            {
                Console.WriteLine("An error occurred: ");
                Console.WriteLine(lookupResponse.Message);
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

    }
}

