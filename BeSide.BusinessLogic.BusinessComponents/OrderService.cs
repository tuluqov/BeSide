using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.BusinessLogic.Construct;
using BeSide.Common.Entities;
using BeSide.DataAccess.Construct;

namespace BeSide.BusinessLogic.BusinessComponents
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork uow;

        public OrderService(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        public void Add(Order order)
        {
            uow.Orders.Create(order);
            uow.Save();
        }

        public void Add(Order order, string userId)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            uow.Orders.Delete(id);
            uow.Save();
        }

        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            var result = uow.Orders.Find(predicate);
            return result;
        }

        public IEnumerable<Order> GetAll()
        {
            var result = uow.Orders.GetAll();
            return result;
        }

        public Order GetById(int id)
        {
            Order findOrder = uow.Orders.GetById(id);
            return findOrder;
        }

        public IEnumerable<Feedback> GetFeedbacks(int orderId)
        {
            Order order = uow.Orders.GetById(orderId);

            return order.Feedbacks;
        }

        public IEnumerable<Order> GetUserOrders(string userId)
        {
            var userOrders = uow.Orders.Find(order => order.ClientProfileId == userId);
            return userOrders;
        }

        public void Update(Order order)
        {
            uow.Orders.Update(order);
            uow.Save();
        }
    }
}
