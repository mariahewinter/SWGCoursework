using FOS.BLL;
using FOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.UI.WorkFlows
{
    public class OrderLookupWorkFlow
    {
        public void Execute(OrderManager manager)
        {
            Console.Clear();
            Console.WriteLine("Look up an order");
            Console.WriteLine(ConsoleIO.SeparatorBar);
            int orderNumber = ConsoleIO.GetRequiredIntFromUser("Enter an order number: ");
            DateTime orderDate = ConsoleIO.GetRequiredLookUpDateTimeFromUser("Enter order date: ");

            OrderLookupResponse response = manager.LookupOrder(orderDate, orderNumber);

            if(response.Success)
            {
                ConsoleIO.DisplayOrderDetails(response.Order);
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
