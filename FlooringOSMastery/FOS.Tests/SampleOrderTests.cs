using FOS.BLL;
using FOS.Models;
using FOS.Models.Responses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Tests
{
    [TestFixture]
    class SampleOrderTests
    {
        [Test]
        public void CanLoadSampleOrderData()
        {
            OrderManager manager = OrderManagerFactory.Create();

            OrderLookupResponse response = manager.LookupOrder(new DateTime(2021, 01, 01), 1);

            Assert.IsNotNull(response.Order);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Order.OrderNumber);
            Assert.AreEqual(new DateTime(2021, 01, 01), response.Order.OrderDate);
        }

        [TestCase("Winter", "OH", 0, "Tile", 100.00, 0, 0, 0, 0, 0, 0, true)] // successful add
        [TestCase("Winter", "TX", 0, "Tile", 100.00, 0, 0, 0, 0, 0, 0, false)] // invalid State
        [TestCase("Winter", "OH", 0, "Plastic", 100.00, 0, 0, 0, 0, 0, 0, false)] // invalid ProductType
        [TestCase("Winter", "OH", 0, "Tile", 50.00, 0, 0, 0, 0, 0, 0, false)] // Area < 100
        [TestCase("", "OH", 0, "Tile", 100.00, 0, 0, 0, 0, 0, 0, false)] // invalid CustomerName
        public void CanAddOrderToOrderList(string customerName, string state, decimal taxRate, string productType, decimal area, decimal costPerSquareFoot, decimal laborCostPerSquareFoot, decimal materialCost, decimal laborCost, decimal tax, decimal total, bool expectedResult)
        {
            OrderManager manager = OrderManagerFactory.Create();

            DateTime date = new DateTime(2021, 01, 01);
            AllOrdersResponse allResponse = manager.RetrieveAllOrders(new DateTime(2021, 01, 01));
            List<Order> orderList = allResponse.Orders;

            Order newOrder = new Order();

            newOrder.OrderDate = new DateTime(2021, 01, 01);
            newOrder.OrderNumber = orderList.Max(order => order.OrderNumber) + 1;
            newOrder.CustomerName = customerName;
            newOrder.State = state;
            newOrder.TaxRate = 0;
            newOrder.ProductType = productType;
            newOrder.Area = area;
            newOrder.CostPerSquareFoot = costPerSquareFoot;
            newOrder.LaborCostPerSquareFoot = laborCostPerSquareFoot;
            newOrder.MaterialCost = materialCost;
            newOrder.LaborCost = laborCost;
            newOrder.Tax = tax;
            newOrder.Total = total;

            AddOrderResponse response = new AddOrderResponse();
            CalculateOrderResponse response2 = manager.CalculateOrder(newOrder);

            if (response2.Success)
            {
                response = manager.AddOrder(response2.Order);
            }

            Assert.AreEqual(expectedResult, response.Success);

        }

        [TestCase("Winter", "OH", "Tile", "200", 0, true)] // successful add
        [TestCase("Weiss", "", "", "", 0, true)] // successful add only name changed
        [TestCase("Weiss", "TX", "", "", 1, true)] // successful add name and state changed
        [TestCase("", "CA", "", "", 1, false)] // invalid State 
        [TestCase("Winter", "OH", "Plastic", "100", 3, false)] // invalid ProductType
        [TestCase("Winter", "OH", "", "50", 2, false)] // Area < 100
        public void CanEditOrder(string editCustomerName, string editStateAbbreviation, string editProductType, string editAreaInput, int changesMade, bool expectedResult)
        {
            OrderManager manager = OrderManagerFactory.Create();

            AllOrdersResponse allResponse = manager.RetrieveAllOrders(new DateTime(2021, 01, 01));
            List<Order> orderList = allResponse.Orders;
            List<Tax> taxList = manager.RetrieveAllTax();
            List<Product> productList = manager.RetrieveAllProducts();
            decimal editArea;

            OrderLookupResponse lookupResponse = manager.LookupOrder(new DateTime(2021, 01, 01), 1);

            if (lookupResponse.Success)
            {
                if (!string.IsNullOrEmpty(editCustomerName)) // IF THEY DID NOT LEAVE IT BLANK, 
                {
                    lookupResponse.Order.CustomerName = editCustomerName; // UPDATE IT TO WHATEVER THEY PUT IN.
                }

                if (!string.IsNullOrEmpty(editStateAbbreviation))
                {
                    lookupResponse.Order.State = editStateAbbreviation.ToUpper();
                }

                if (!string.IsNullOrEmpty(editProductType))
                {
                    lookupResponse.Order.ProductType = editProductType;
                }

                if (!string.IsNullOrEmpty(editAreaInput)) // IF THE AREA STRING IS NOT BLANK
                {
                    if (!decimal.TryParse(editAreaInput, out editArea)) // CHECK TO MAKE SURE THE STRING CAN BE PARSED TO DECIMAL
                    {                                                   // IF IT CAN'T, TELL THEM TO LEAVE IT UNCHANGED OR GIVE A VALID DECIMAL
                        lookupResponse.Success = false;
                    }
                    else
                    {
                        if (editArea < 100) // IF THEY'VE PROVIDED A VALID DECIMAL TO CHANGE IT TO, CHECK IT'S OVER 100.
                        {
                            lookupResponse.Success = false;
                        }
                        lookupResponse.Order.Area = editArea; // UPDATE THE AREA TO WHATEVER THEY PUT IN.
                    }
        
                    if(changesMade == 0)
                    {
                        AddOrderResponse addResponse1 = new AddOrderResponse(); 
                        addResponse1 = manager.AddOrder(lookupResponse.Order);

                        Assert.AreEqual(expectedResult, addResponse1.Success);
                    }
                    else if (changesMade != 0)
                    {
                        AddOrderResponse addResponse2 = new AddOrderResponse();
                        CalculateOrderResponse calculateResponse = manager.CalculateOrder(lookupResponse.Order);

                        if (calculateResponse.Success)
                        {
                            addResponse2 = manager.AddOrder(calculateResponse.Order);
                        }

                        Assert.AreEqual(expectedResult, addResponse2.Success);
                    }
                    

                }
            }
        }
        [TestCase(2, true)] // successful removal
        [TestCase(4, false)] // invalid order number not on date -- can't remove
        public void CanRemoveOrder(int orderNumber, bool expectedResult)
        {
            OrderManager manager = OrderManagerFactory.Create();
            AllOrdersResponse allResponse = manager.RetrieveAllOrders(new DateTime(2021, 01, 01));
            List<Order> allOrders = allResponse.Orders;

            OrderLookupResponse lookupResponse = manager.LookupOrder(new DateTime(2021,01,01), orderNumber);
            RemoveOrderResponse removeResponse = new RemoveOrderResponse();

            if (lookupResponse.Success)
            {
                removeResponse = manager.RemoveOrder(lookupResponse.Order, new DateTime(2021, 01, 01));
            }

            Assert.AreEqual(expectedResult, removeResponse.Success);
        }
    }
}
