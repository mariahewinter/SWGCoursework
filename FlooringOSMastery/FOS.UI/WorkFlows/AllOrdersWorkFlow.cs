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
    public class AllOrdersWorkFlow
    {
        public void Execute(OrderManager manager)
        {
            Console.Clear();
            Console.WriteLine("Look up all orders for date:");
            Console.WriteLine(ConsoleIO.SeparatorBar);
            DateTime orderDate = ConsoleIO.GetRequiredLookUpDateTimeFromUser("Enter order date: ");

            AllOrdersResponse response = manager.RetrieveAllOrders(orderDate);

            if (response.Success)
            {
                foreach(Order order in response.Orders)
                {
                    ConsoleIO.DisplayOrderDetails(order);
                }
            }
            else
            {
                Console.WriteLine("An error occurred: ");
                Console.WriteLine(response.Message);
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
