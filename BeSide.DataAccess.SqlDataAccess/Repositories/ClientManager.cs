using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.Common.Entities;
using BeSide.DataAccess.Construct;
using BeSide.DataAccess.SqlDataAccess.DataContexts;

namespace BeSide.DataAccess.SqlDataAccess.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }

        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(User item)
        {
            Database.UsersProfile.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
