using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BeSide.BusinessLogic.Construct.DTO;
using BeSide.BusinessLogic.Construct.Infrastructure;
using BeSide.Common.Entities;

namespace BeSide.BusinessLogic.Construct
{
    public interface IUserService
    {
        OperationDetails Create(UserDto user);
        Task<ClaimsIdentity> Authenticate(UserDto user);
        Task SetInitialData(UserDto admin, List<string> roles);

        ApplicationUser GetById(string id);
        void UpdateProvider(BaseProfile provider);
        void UpdateClient(BaseProfile client);
        void UpdateUser(ApplicationUser user);
        IEnumerable<ProviderProfile> GetAllProviders();
        IEnumerable<ProviderProfile> FindProviders(Func<ProviderProfile, bool> predicate);
    }
}
