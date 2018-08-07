using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeSide.Presenter.WebSite.Models.Service
{
    public class ServiceCollectionViewModel : IEnumerable<ServiceViewModel>
    {
        private IList<ServiceViewModel> list;

        public ServiceCollectionViewModel(IEnumerable<Common.Entities.Service> services)
        {
            list = new List<ServiceViewModel>();

            foreach (Common.Entities.Service service in services)
            {
                list.Add(new ServiceViewModel(service));
            }
        }

        public IEnumerator<ServiceViewModel> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}