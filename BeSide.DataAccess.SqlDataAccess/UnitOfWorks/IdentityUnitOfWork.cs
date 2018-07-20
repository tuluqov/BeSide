using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.Common.Entities;
using BeSide.DataAccess.Construct;
using BeSide.DataAccess.SqlDataAccess.DataContexts;
using BeSide.DataAccess.SqlDataAccess.Identity;
using BeSide.DataAccess.SqlDataAccess.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeSide.DataAccess.SqlDataAccess.UnitOfWorks
{
    public class IdentityUnitOfWork : IUserUnitOfWork<ApplicationUserManager, ApplicationRoleManager>
    {
        private ApplicationContext db;

        private readonly ApplicationUserManager userManager;
        private readonly ApplicationRoleManager roleManager;
        private readonly IClientManager clientManager;

        public IdentityUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
        }

        #region Realization interace
        

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                }
                this.disposed = true;
            }
        }

        #endregion
    }
}
