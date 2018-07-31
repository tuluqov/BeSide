using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.Common.Entities;

namespace BeSide.BusinessLogic.Construct
{
    public interface IOrderService
    {
        Order GetById(int id);
        void Add(Order order);
        void Add(Order order, string userId);
        void Update(Order order);
        IEnumerable<Order> GetAll();
        IEnumerable<Order> Find(Func<Order, bool> predicate);
        IEnumerable<Order> GetUserOrders(string userId);
    }
}
