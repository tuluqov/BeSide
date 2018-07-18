using System.Collections.Generic;

namespace BeSide.Common.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }

        public ICollection<Order> Orders { get; set; }

        public int ApplicationUserId { get; set; }
        public ApplicationUser UserIdentity { get; set; }
    }
}
