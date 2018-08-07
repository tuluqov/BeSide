using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BeSide.Common.Entities;
using BeSide.Presenter.WebSite.Models.Feedback;
using BeSide.Presenter.WebSite.Models.Service;

namespace BeSide.Presenter.WebSite.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Краткое описание")]
        public string ShortDescription { get; set; }

        [Required]
        [MaxLength(1000)]
        [Display(Name = "Полное описание")]
        public string FullDescription { get; set; }

        [MaxLength(40)]
        [Display(Name = "Имя")]
        public string NameProvider { get; set; }

        [Display(Name = "Цена")]
        public float? Price { get; set; }

        [Display(Name = "Договорная")]
        public bool ContractPrice { get; set; }

        [Required]
        [Display(Name = "Дата исполнения")]
        public DateTime Deadline { get; set; }

        [Display(Name = "Дата публикации")]
        public DateTime CreateDate { get; set; }

        [MaxLength(15)]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "Город")]
        public string City { get; set; }

        //Разместил заказ
        public string ClientProfileId { get; set; }

        //Исполняет
        public string ProviderProfileId { get; set; }

        [Required]
        [Display(Name = "Тип услуги")]
        public int ServiceId { get; set; }

        public ServiceViewModel ServiceModel { get; set; }

        public FeedbacksCollectionViewModel Feedbacks { get; set; }

        public OrderViewModel()
        {
            
        }

        public OrderViewModel(Common.Entities.Order order)
        {
            Id = order.Id;
            City = order.City;
            ShortDescription = order.ShortDescription;
            FullDescription = order.FullDescription;
            NameProvider = order.NameProvider;
            ContractPrice = order.ContractPrice;
            Deadline = order.Deadline;
            Price = order.Price;
            CreateDate = order.CreateDate;

            ClientProfileId = order.ClientProfileId;
            ProviderProfileId = order.ProviderProfileId;

            ServiceId = order.ServiceId;
            ServiceModel = new ServiceViewModel(order.Service);

            Feedbacks = new FeedbacksCollectionViewModel(order.Feedbacks);
        }

        public Common.Entities.Order GetOrder()
        {
            Common.Entities.Order order = new Common.Entities.Order
            {
                Id = Id,
                ShortDescription = ShortDescription,
                FullDescription = FullDescription,
                City = City,
                ContractPrice = ContractPrice,
                Price = Price,
                Deadline = Deadline,
                CreateDate = CreateDate,
                NameProvider = NameProvider,
                ClientProfileId = ClientProfileId,
                ProviderProfileId = ProviderProfileId,
                PhoneNumber = PhoneNumber,

                ServiceId = ServiceId
            };

            return order;
        }
    }
}