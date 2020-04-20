using FOS.Models;
using FOS.Models.Interfaces;
using FOS.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;
        private ITaxRepository _taxRepository;
        private IProductRepository _productRepository;

        public OrderManager(IOrderRepository orderRepository, ITaxRepository taxRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _taxRepository = taxRepository;
            _productRepository = productRepository;
        }

        public AllOrdersResponse RetrieveAllOrders(DateTime orderDate)
        {
            AllOrdersResponse response = new AllOrdersResponse();
            List<Order> orderList = _orderRepository.LoadAllOrders(orderDate);

            if (orderList.Count == 0)
            {
                response.Success = false;
                response.Message = $"No orders have been placed for {orderDate}.";
            }
            else
            {
                response.Success = true;
                response.Orders = orderList;
            }
            return response;
        }
        public List<Product> RetrieveAllProducts()
        {
            return _productRepository.LoadAllProducts();
        }

        public List<Tax> RetrieveAllTax()
        {
            return _taxRepository.LoadAllTax();
        }
        public OrderLookupResponse LookupOrder(DateTime date, int orderNumber)
        {
            OrderLookupResponse response = new OrderLookupResponse();
            List<Order> orderList = _orderRepository.LoadAllOrders(date);

            if (!orderList.Exists(order => order.OrderNumber == orderNumber))
            {
                response.Success = false;
                response.Message = $"{ orderNumber} is not a valid order number within { date}.";
                return response;
            }
            else
            {
                response.Order = orderList.Where(order => order.OrderNumber == orderNumber).ToList()[0];
                response.Success = true;
            }
            return response;
        }

        public CalculateOrderResponse CalculateOrder(Order newOrder)
        {
            CalculateOrderResponse response = new CalculateOrderResponse();
            List<Product> productList = _productRepository.LoadAllProducts();
            List<Tax> taxList = _taxRepository.LoadAllTax();

            if (string.IsNullOrEmpty(newOrder.CustomerName))
            {
                response.Success = false;
                response.Message = $"Customer Name can not be blank.";
            }
            else if (newOrder.Area < 100)
            {
                response.Success = false;
                response.Message = $"{newOrder.Area} is not greater than the minimum 100 area.";
            }
            else if (productList.Where(product => product.ProductType == newOrder.ProductType).ToList().Count == 0)
            {
                response.Success = false;
                response.Message = $"{newOrder.ProductType} is not a valid product type.";
            }
            else if (taxList.Where(tax => tax.StateAbbreviation == newOrder.State.ToUpper()).ToList().Count == 0)
            {
                response.Success = false;
                response.Message = $"{newOrder.State} is not serviced by this company.";
            }
            else
            {
                Tax taxCalculations = taxList.Where(tax => tax.StateAbbreviation == newOrder.State.ToUpper()).ToList()[0]; // SET CORRECT TAXRATE VALUE FOR CALCULATION

                newOrder.TaxRate = taxCalculations.TaxRate;

                Product productCalculations = productList.Where(product => product.ProductType == newOrder.ProductType).ToList()[0]; // SET CORRECT PRODUCT COST / PRODUCT LABOR COST VALUES FOR CALCULATION

                newOrder.CostPerSquareFoot = productCalculations.CostPerSquareFoot;
                newOrder.LaborCostPerSquareFoot = productCalculations.LaborCostPerSquareFoot;

                // CALCULATE
                newOrder.MaterialCost = (newOrder.Area * newOrder.CostPerSquareFoot);
                newOrder.LaborCost = (newOrder.Area * newOrder.LaborCostPerSquareFoot);
                newOrder.Tax = ((newOrder.MaterialCost + newOrder.LaborCost) * (newOrder.TaxRate / 100));
                newOrder.Total = (newOrder.MaterialCost + newOrder.LaborCost + newOrder.Tax);

                response.Order = newOrder;
                response.Success = true;
            }
            return response;

        }

        public AddOrderResponse AddOrder(Order order)
        {
            AddOrderResponse response = new AddOrderResponse();
            List<Product> productList = _productRepository.LoadAllProducts();
            List<Tax> taxList = _taxRepository.LoadAllTax();

            if (order.CustomerName == null)
            {
                response.Success = false;
                response.Message = $"{order.CustomerName} can not be blank.";
            }
            else if (order.Area < 100)
            {
                response.Success = false;
                response.Message = $"{order.Area} is not greater than the minimum 100 area.";
            }
            else if (productList.Where(product => product.ProductType == order.ProductType).ToList().Count == 0)
            {
                response.Success = false;
                response.Message = $"{order.ProductType} is not a valid product type.";
            }
            else if (taxList.Where(tax => tax.StateAbbreviation == order.State.ToUpper()).ToList().Count == 0)
            {
                response.Success = false;
                response.Message = $"{order.State} is not serviced by this company.";
            }
            else
            {
                _orderRepository.SaveOrder(order);
                response.Order = order;
                response.Success = true;
            }
            return response;

        }

        public RemoveOrderResponse RemoveOrder(Order order, DateTime date)
        {
            RemoveOrderResponse response = new RemoveOrderResponse();
            List<Order> allOrders = _orderRepository.LoadAllOrders(date);
            if (!allOrders.Exists(o => o.OrderNumber == order.OrderNumber))
            {
                response.Success = false;
                response.Message = $"Order {order.OrderNumber} does not exist for {date}.";
            }
            else if (allOrders.Count == 0)
            {
                response.Success = false;
                response.Message = $"No orders exist for {date}";
            }
            else
            {
                _orderRepository.DeleteOrder(order);
                response.Success = true;
            }
            return response;
        }
    }
}
