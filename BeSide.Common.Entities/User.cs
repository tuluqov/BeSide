using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSide.Common.Entities
{
    public class User : BaseEntity
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public override int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
