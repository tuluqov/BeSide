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
        public string ClientProfileId { get; set; }
        [ForeignKey(nameof(ClientProfileId))]
        public virtual ClientProfile ClientProfile { get; set; }

        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }
    }
}
