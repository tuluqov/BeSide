using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeSide.Common.Entities
{
    public class Category : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
