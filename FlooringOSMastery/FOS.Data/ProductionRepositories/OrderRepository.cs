using FOS.Models;
using FOS.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Data.ProductionRepositories
{
    public class OrderRepository : IOrderRepository
    {
        private string _filePath = @"C:\Repos\net-mpls-0120-classwork-mariahewinter\FlooringOSMastery\Orders_";

        private string MapOrdersToLine(Order order)
        {
            return order.OrderDate + "|" + order.OrderNumber + "|" + order.CustomerName + "|" + order.State + "|" + order.TaxRate + "|" + order.ProductType + "|" + order.Area + "|" + order.CostPerSquareFoot + "|" + order.LaborCostPerSquareFoot + "|" + order.MaterialCost + "|" + order.LaborCost + "|" + order.Tax + "|" + order.Total;
        }
        private void WriteAllOrders(List<Order> orderList, DateTime date)
        {
            if(orderList.Count > 0)
            {
                using (StreamWriter writer = new StreamWriter(_filePath + orderList[0].OrderDate.ToString("MMddyyyy") + ".txt"))
                {
                    writer.WriteLine("OrderDate,OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                    foreach (Order order in orderList)
                    {
                        writer.WriteLine(MapOrdersToLine(order));
                    }
                }
            }
            else
            {
                File.Delete(_filePath + date.ToString("MMddyyyy") + ".txt");
            }
           
        }

        private Order MapLineToOrderList(string line)
        {
            string[] columns = line.Split('|');
            Order order = new Order();
            order.OrderDate = Convert.ToDateTime(columns[0]);
            order.OrderNumber = Convert.ToInt32(columns[1]);
            order.CustomerName = columns[2];
            order.State = columns[3];
            order.TaxRate = Convert.ToDecimal(columns[4]);
            order.ProductType = columns[5];
            order.Area = Convert.ToDecimal(columns[6]);
            order.CostPerSquareFoot = Convert.ToDecimal(columns[7]);
            order.LaborCostPerSquareFoot = Convert.ToDecimal(columns[8]);
            order.MaterialCost = Convert.ToDecimal(columns[9]);
            order.LaborCost = Convert.ToDecimal(columns[10]);
            order.Tax = Convert.ToDecimal(columns[11]);
            order.Total = Convert.ToDecimal(columns[12]);

            return order;
        }

        public List<Order> LoadAllOrders(DateTime date)
        {
            List<Order> orderList = new List<Order>();
            try
            {
                using (StreamReader reader = new StreamReader(_filePath + date.ToString("MMddyyyy") + ".txt"))
                {
                    string line;
                    reader.ReadLine(); // skip header when reading in
                    while ((line = reader.ReadLine()) != null)
                    {
                        orderList.Add(MapLineToOrderList(line));
                    }
                }
            }
            catch 
            {

            }
            return orderList;

        }

        public void SaveOrder(Order order)
        {
            List<Order> orderList = LoadAllOrders(order.OrderDate).ToList();

            if(order.OrderNumber == 0)
            {
                if(orderList.Count == 0)
                {
                    order.OrderNumber = 1;
                }
                else
                {
                    order.OrderNumber = orderList.Max(o => o.OrderNumber) + 1;
                }

            }
            else
            {
                orderList.Remove(orderList.Where(order1 => order1.OrderNumber == order.OrderNumber).FirstOrDefault());
            }
            orderList.Add(order);
            WriteAllOrders(orderList, order.OrderDate);
        }

        public void DeleteOrder(Order order)
        {
            List<Order> orderList = LoadAllOrders(order.OrderDate).ToList();

            orderList.Remove(orderList.Where(order1 => order1.OrderNumber == order.OrderNumber).FirstOrDefault());

            WriteAllOrders(orderList, order.OrderDate);
        }
    }
}
