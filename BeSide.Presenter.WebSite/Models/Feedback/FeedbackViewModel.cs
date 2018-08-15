using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BeSide.Common.Entities;
using Newtonsoft.Json;

namespace BeSide.Presenter.WebSite.Models.Feedback
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }

        public string ProviderProfileId { get; set; }

        public ProviderProfile ProviderProfile { get; set; }

        public int OrderId { get; set; }

        [MinLength(5)]
        [MaxLength(200)]
        [Display(Name = "Описание")]
        public string Text { get; set; }


        [Range(1, float.MaxValue)]
        [Display(Name = "Цена")]
        public float Price { get; set; }

        public DateTime CreateDate { get; set; }


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
            ProviderProfile = feedback.ProviderProfile;
            CreateDate = feedback.CreateDate;
        }

        public Common.Entities.Feedback GetFeedback()
        {
            return new Common.Entities.Feedback
            {
                Id = Id,
                OrderId = OrderId,
                ProviderProfileId = ProviderProfileId,
                Text = Text,
                Price = Price,
                CreateDate = CreateDate,
            };
        }
    }
}