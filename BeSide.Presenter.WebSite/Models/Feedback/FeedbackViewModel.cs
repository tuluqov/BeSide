using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeSide.Common.Entities;

namespace BeSide.Presenter.WebSite.Models.Feedback
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }

        public string ProviderProfileId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int OrderId { get; set; }

        public string Text { get; set; }

        public float Price { get; set; }


        public FeedbackViewModel()
        {

        }

        public FeedbackViewModel(Common.Entities.Feedback feedback)
        {
            Id = feedback.Id;
            ProviderProfileId = feedback.ProviderProfileId;
            OrderId = feedback.OrderId;
            Text = feedback.Text;
            Price = feedback.Price;
        }

        public Common.Entities.Feedback GetFeedback()
        {
            return new Common.Entities.Feedback
            {
                Id = Id,
                OrderId = OrderId,
                ProviderProfileId = ProviderProfileId,
                Text = Text,
                Price = Price
            };
        }
    }
}