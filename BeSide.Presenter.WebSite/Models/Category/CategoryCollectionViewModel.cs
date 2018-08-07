using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeSide.Presenter.WebSite.Models.Category
{
    public class CategoryCollectionViewModel : IEnumerable<CategoryViewModel>
    {
        private IList<CategoryViewModel> list;

        public CategoryCollectionViewModel(IEnumerable<Common.Entities.Category> categories)
        {
            list = new List<CategoryViewModel>();

            foreach (Common.Entities.Category category in categories)
            {
                list.Add(new CategoryViewModel(category));
            }
        }

        public IEnumerator<CategoryViewModel> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}