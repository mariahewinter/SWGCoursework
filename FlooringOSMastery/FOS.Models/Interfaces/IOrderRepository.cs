using FOS.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Models
{
    public interface IOrderRepository
    {
        List<Order> LoadAllOrders(DateTime date);
        void SaveOrder(Order order);
        void DeleteOrder(Order order);
    }
}
