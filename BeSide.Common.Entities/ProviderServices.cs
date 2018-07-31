using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSide.Common.Entities
{
    public class ProviderServices : BaseEntity
    {
        [Required]
        public string ClientProfileId { get; set; }
        [ForeignKey(nameof(ClientProfileId))]
        public virtual ClientProfile ClientProfile { get; set; }

        public int ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; set; }
    }
}
