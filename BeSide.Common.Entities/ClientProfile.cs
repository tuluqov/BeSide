using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSide.Common.Entities
{
    public class ClientProfile : BaseProfile
    {
        public virtual ICollection<Order> Orders { get; set; }
    }
}
