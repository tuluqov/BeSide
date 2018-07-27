using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSide.Common.Entities
{
    public class ProviderProfile : BaseProfile
    {
        public string CompanyName { get; set; }

        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
