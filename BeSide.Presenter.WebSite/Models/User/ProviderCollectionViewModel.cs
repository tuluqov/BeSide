using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeSide.Common.Entities;

namespace BeSide.Presenter.WebSite.Models.User
{
    public class ProviderCollectionViewModel : IEnumerable<ProviderViewModel>
    {
        private IList<ProviderViewModel> list;

        public ProviderCollectionViewModel(IEnumerable<ProviderProfile> providers)
        {
            list = new List<ProviderViewModel>();

            foreach (ProviderProfile profile in providers)
            {
                list.Add(new ProviderViewModel(profile));
            }
        }

        public IEnumerator<ProviderViewModel> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}