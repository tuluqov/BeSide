using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BeSide.BusinessLogic.BusinessComponents.Infrastructure;
using BeSide.BusinessLogic.Construct.DTO;
using BeSide.Common.Entities;

namespace BeSide.BusinessLogic.Construct
{
    public interface IUserService
    {
        Task<OperationDetails> Create(UserDto user);
        Task<ClaimsIdentity> Authenticate(UserDto user);
        Task SetInitialData(UserDto admin, List<string> roles);
    }
}
