using Microsoft.AspNet.Identity.EntityFramework;

namespace BeSide.Common.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
