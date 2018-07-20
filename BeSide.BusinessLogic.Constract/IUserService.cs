using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BeSide.Common.Entities;

namespace BeSide.BusinessLogic.Construct
{
    public interface IUserService : IDisposable
    {
        Task<User> Create(User user);
        Task<ClaimsIdentity> Authenticate(User user);
    }
}
