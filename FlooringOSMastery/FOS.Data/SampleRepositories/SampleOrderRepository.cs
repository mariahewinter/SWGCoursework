using FOS.Models;
using FOS.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Data
{
    public class SampleOrderRepository : IOrderRepository
    {
        DateTime orderDate = new DateTime(2021, 01, 01);

        List<Order> _sampleOrders = new List<Order>();

        public SampleOrderRepository()
        {
            _sampleOrders.Add(new Order() { OrderDate = new DateTime(2021, 01, 01), OrderNumber = 1, CustomerName = "Wise", State = "OH", TaxRate = 6.25M, ProductType = "Wood", Area = 100.00M, CostPerSquareFoot = 5.15M, LaborCostPerSquareFoot = 4.75M, MaterialCost = 515.00M, LaborCost = 475.00M, Tax = 61.88M, Total = 1051.88M});
            _sampleOrders.Add(new Order() { OrderDate = new DateTime(2021, 01, 01), OrderNumber = 2, CustomerName = "Winter", State = "OH", TaxRate = 6.25M, ProductType = "Wood", Area = 100.00M, CostPerSquareFoot = 5.15M, LaborCostPerSquareFoot = 4.75M, MaterialCost = 515.00M, LaborCost = 475.00M, Tax = 61.88M, Total = 1051.88M });
        }

        public List<Order> LoadAllOrders(DateTime date)
        {
            return _sampleOrders;
        }
        public Order LoadOrder(DateTime date, int orderNumber)
        {
            if (date == orderDate)
            {
                List<Order> orders = _sampleOrders.Where(order => order.OrderNumber == orderNumber).ToList();
                Order requestedOrder = orders[0];
                return requestedOrder;
            }
            else
            {
                return null;
            }
        }

        public void SaveOrder(Order order)
        {

            if(order.OrderNumber == 0)
            {
                order.OrderNumber = _sampleOrders.Max(o => o.OrderNumber) + 1;
            }
            if(_sampleOrders.Contains(order))
            {
                _sampleOrders.RemoveAll(orders => orders.OrderNumber == order.OrderNumber);
            }
            _sampleOrders.Add(order);
        }

        public void DeleteOrder(Order order)
        {
            _sampleOrders.RemoveAll(orders => orders.OrderNumber == order.OrderNumber);
        }
    }
}
