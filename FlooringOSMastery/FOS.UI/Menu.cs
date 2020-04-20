using FOS.BLL;
using FOS.UI.WorkFlows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.UI
{
    public static class Menu
    {
        public static void Start()
        {
            OrderManager manager = OrderManagerFactory.Create();

            while (true)
            {
                DisplayMenu();
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        OrderLookupWorkFlow lookupWorkFlow = new OrderLookupWorkFlow();
                        lookupWorkFlow.Execute(manager);
                        break;
                    case "2":
                        AllOrdersWorkFlow allOrdersWorkFlow = new AllOrdersWorkFlow();
                        allOrdersWorkFlow.Execute(manager);
                        break;
                    case "3":
                        AddOrderWorkFlow addWorkFlow = new AddOrderWorkFlow();
                        addWorkFlow.Execute(manager);
                        break;
                    case "4":
                        EditOrderWorkFlow editWorkFlow = new EditOrderWorkFlow();
                        editWorkFlow.Execute(manager);
                        break;
                    case "5":
                        RemoveOrderWorkFlow removeWorkFlow = new RemoveOrderWorkFlow();
                        removeWorkFlow.Execute(manager);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Please enter a valid choice. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("****************************");
            Console.WriteLine("Flooring Program\n");
            Console.WriteLine("1. Display Order by Date and Order Number");
            Console.WriteLine("2. Display all Orders by given Date");
            Console.WriteLine("3. Add an Order");
            Console.WriteLine("4. Edit an Order");
            Console.WriteLine("5. Remove an Order");
            Console.WriteLine("6. Quit");

            Console.Write("\n Enter selection: ");
        }
    }
}
