using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSide.Common.Entities
{
    public class ProviderProfile : BaseProfile
    {
        public virtual ICollection<Service> Services { get; set; }
    }
}
