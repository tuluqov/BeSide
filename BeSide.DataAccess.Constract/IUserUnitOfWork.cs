using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSide.DataAccess.Construct
{
    public interface IUserUnitOfWork<out TAppUserManager, out TAppRoleManager> : IDisposable
    {
        TAppUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        TAppRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}
