using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSide.Common.Entities
{
    public class Feedback : BaseEntity
    {
        [Required]
        public string ProviderProfileId { get; set; }
        [ForeignKey(nameof(ProviderProfileId))]
        public virtual ProviderProfile ProviderProfile { get; set; }

        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [MaxLength(200)]
        public string Text { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        public float Price { get; set; }
    }
}
