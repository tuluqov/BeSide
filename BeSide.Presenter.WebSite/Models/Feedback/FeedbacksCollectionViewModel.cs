using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeSide.Presenter.WebSite.Models.Feedback
{
    public class FeedbacksCollectionViewModel : IEnumerable<FeedbackViewModel>
    {
        private IList<FeedbackViewModel> list;

        public FeedbacksCollectionViewModel(IEnumerable<Common.Entities.Feedback> feedbacks)
        {
            list = new List<FeedbackViewModel>();

            foreach (Common.Entities.Feedback feedback in feedbacks)
            {
                list.Add(new FeedbackViewModel(feedback));
            }
        }

        public IEnumerator<FeedbackViewModel> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}