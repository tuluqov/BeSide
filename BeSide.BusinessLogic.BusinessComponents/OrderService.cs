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
        private IUnitOfWork uow;

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
            var result = uow.Orders.GetById(id);
            return result;
        }

        public IEnumerable<Order> GetUserOrders(string userId)
        {
            ClientProfile user = uow.ClientProfiles.GetById(userId);

            var result = user.Orders.ToList();

            return result;
        }

        public void Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
