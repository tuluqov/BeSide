using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSide.Common.Entities
{
    public class ProviderServices : BaseEntity
    {
        [Required]
        public string ProviderProfileId { get; set; }
        [ForeignKey(nameof(ProviderProfileId))]
        public virtual ProviderProfile ProviderProfile { get; set; }

        public int ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; set; }
    }
}
