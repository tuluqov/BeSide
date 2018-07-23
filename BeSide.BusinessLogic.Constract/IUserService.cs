using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BeSide.BusinessLogic.BusinessComponents.Infrastructure;
using BeSide.Common.Entities.DTO;

namespace BeSide.BusinessLogic.Construct
{
    public interface IUserService
    {
        Task<OperationDetails> Create(UserDto user);
        Task<ClaimsIdentity> Authenticate(UserDto user);
        Task SetInitialData(UserDto admin, List<string> roles);
    }
}
