using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeSide.Common.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual BaseProfile UserProfile { get; set; }
    }
}
