using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSide.Common.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string ShortDescription { get; set; }
        [Required]
        [MaxLength(1000)]
        public string FullDescription { get; set; }

        [MaxLength(40)]
        public string NameProvider { get; set; }

        public int? Price { get; set; }

        public bool ContractPrice { get; set; }

        public DateTime? Deadline { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(40)]
        public string City { get; set; }
        

        //Разместил заказ
        public string ClientProfileId { get; set; }
        [ForeignKey(nameof(ClientProfileId))]
        public virtual ClientProfile ClientProfile { get; set; }

        //Исполняет
        public string ProviderProfileId { get; set; }
        [ForeignKey(nameof(ProviderProfileId))]
        public virtual ProviderProfile ProviderProfile { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
