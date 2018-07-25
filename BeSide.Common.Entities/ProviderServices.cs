using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSide.Common.Entities
{
    public class ProviderServices : BaseEntity
    {
        [Required]
        public string ProviderId { get; set; }
        [ForeignKey(nameof(ProviderId))]
        public virtual UserProfile Provider { get; set; }

        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }
    }
}
