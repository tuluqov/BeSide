using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeSide.Presenter.WebSite.Models.Order
{
    public class OrderCollectionViewModel : IEnumerable<OrderViewModel>
    {
        private IList<OrderViewModel> list;

        public OrderCollectionViewModel(IEnumerable<Common.Entities.Order> orders)
        {
            list = new List<OrderViewModel>();

            foreach (Common.Entities.Order order in orders)
            {
                list.Add(new OrderViewModel(order));
            }
        }

        public IEnumerator<OrderViewModel> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}