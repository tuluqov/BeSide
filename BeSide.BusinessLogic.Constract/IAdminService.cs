using System.Collections.Generic;
using BeSide.Common.Entities;

namespace BeSide.BusinessLogic.Construct
{
    public interface IAdminService
    {
        IEnumerable<ApplicationUser> GetAllUsers();
    }
}
