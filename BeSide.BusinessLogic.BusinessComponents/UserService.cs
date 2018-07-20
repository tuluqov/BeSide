using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BeSide.BusinessLogic.Construct;
using BeSide.Common.Entities;
using BeSide.DataAccess.Construct;
using BeSide.DataAccess.SqlDataAccess.Identity;

namespace BeSide.BusinessLogic.BusinessComponents
{
    public class UserService : IUserService
    {
        private IUserUnitOfWork<ApplicationUserManager, ApplicationRoleManager> DataBase { get; set; }

        public UserService(IUserUnitOfWork<ApplicationUserManager, ApplicationRoleManager> uow)
        {
            DataBase = uow;
        }

        public Task<ClaimsIdentity> Authenticate(User user)
        {
            throw new NotImplementedException();
        }

        public Task<BeSide.Common.Entities.User> Create(BeSide.Common.Entities.User user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
