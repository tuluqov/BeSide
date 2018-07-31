using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSide.Common.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        public virtual ICollection<ClientProfile> ClientProfiles { get; set; }
    }
}
