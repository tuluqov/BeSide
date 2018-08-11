using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSide.Common.Entities
{
    public class ProviderProfile : BaseProfile
    {
        [MaxLength(100)]
        public string CompanyName { get; set; }

        [MaxLength(300)]
        public string Discription { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
