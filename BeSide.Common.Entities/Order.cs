using System;
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
        public int? Price { get; set; }
        public DateTime? Deadline { get; set; }
        public string PhoneNumber { get; set; }

        public int IdService { get; set; }
        public virtual Service Service { get; set; }

        //Предоставляет услугу
        [Required]
        public string IdProvider { get; set; }
        [ForeignKey(nameof(IdProvider))]
        public virtual UserProfile Provider { get; set; }

        //Выполняет услугу
        public string IdClient { get; set; }
        [ForeignKey(nameof(IdClient))]
        public virtual UserProfile Client { get; set; }
    }
}
