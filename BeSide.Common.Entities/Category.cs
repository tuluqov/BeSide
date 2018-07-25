using System.Collections.Generic;

namespace BeSide.Common.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
